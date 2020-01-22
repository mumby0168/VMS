using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Exceptions;
using Services.Common.Jwt;
using Services.Common.Logging;
using Services.Identity.Domain;
using Services.Identity.Managers;
using Services.Identity.Messages.Events;
using Services.Identity.Models;
using Services.Identity.Repositorys;
using Services.RabbitMq.Messages;

namespace Services.Identity.Services
{
    public class AdminIdentityService : IAdminIdentityService
    {
        private readonly IServiceBusMessagePublisher _serviceBus;
        private readonly IIdentityRepository _identityRepository;
        private readonly IPendingIdentityRepository _pendingIdentityRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly IJwtManager _jwtManager;
        private readonly IVmsLogger<AdminIdentityService> _logger;
        private readonly IRefreshTokenService _tokenService;
        private readonly IBusinessRepository _businessRepository;

        public AdminIdentityService(IServiceBusMessagePublisher serviceBus, IIdentityRepository identityRepository,
            IPendingIdentityRepository pendingIdentityRepository, IPasswordManager passwordManager,
            IJwtManager jwtManager, IVmsLogger<AdminIdentityService> logger, IRefreshTokenService tokenService,
            IBusinessRepository businessRepository)
        {
            _serviceBus = serviceBus;
            _identityRepository = identityRepository;
            _pendingIdentityRepository = pendingIdentityRepository;
            _passwordManager = passwordManager;
            _jwtManager = jwtManager;
            _logger = logger;
            _tokenService = tokenService;
            _businessRepository = businessRepository;
        }

        public async Task<IAuthToken> SignIn(string email, string password, string role)
        {
            var identity = await _identityRepository.GetByEmailAndRole(email, role);
            if (identity is null)
            {
                _logger.LogWarning($"No user found with email: {email} role: {role}");
                throw new VmsException(Codes.InvalidCredentials, "The credentials provided where incorrect.");
            }


            if (!_passwordManager.IsPasswordCorrect(password, identity.Hash, identity.Salt))
            {
                _logger.LogWarning($"Incorrect password for: {email}");
                throw new VmsException(Codes.InvalidCredentials, "The credentials provided where incorrect.");
            }


            var jwt = _jwtManager.CreateToken(identity.Id, identity.Email, identity.Role);
            var refreshToken = await _tokenService.CreateRefreshToken(identity.Email);

            _logger.LogInformation($"User issued token email: {email}");

            return AuthToken.Create(jwt, refreshToken);
        }

        public async Task CreateAdmin(string email)
        {
            if (await _identityRepository.IsEmailInUse(email, Roles.SystemAdmin) ||
                await _pendingIdentityRepository.IsEmailInUse(email, Roles.SystemAdmin))
                throw new VmsException(Codes.EmailInUse, "The email supplied is already in use.");

            var pending = new PendingIdentity(Guid.NewGuid(), email, Roles.SystemAdmin);

            await _pendingIdentityRepository.AddAsync(pending);

            _logger.LogInformation($"create code issued: {pending.Id}.");

            _serviceBus.PublishEvent(new PendingAdminCreated(pending.Id, pending.Email), RequestInfo.Empty);
        }

        public async Task CompleteAdmin(Guid code, string password, string passwordMatch, string email)
        {
            var pendingIdentity = await _pendingIdentityRepository.GetAsync(code, email);
            if (pendingIdentity is null)
                throw new VmsException(Codes.InvalidCredentials, "The credentials are invalid.");

            var existing = await _identityRepository.GetByEmailAndRole(email, Roles.SystemAdmin);
            if (existing != null)
                throw new VmsException(Codes.EmailInUse, "Their has already been an account created with this email.");

            if (password != passwordMatch)
                throw new VmsException(Codes.InvalidCredentials, "The credentials are invalid.");

            var pword = _passwordManager.EncryptPassword(password);
            var identity = new Domain.Identity(email, pword.Hash, pword.Salt, Roles.SystemAdmin);

            await _identityRepository.AddAsync(identity);
        }

        public async Task CreateBusinessAdmin(string email, Guid businessId)
        {
            if (await _identityRepository.IsEmailInUse(email, Roles.BusinessAdmin) ||
                await _pendingIdentityRepository.IsEmailInUse(email, Roles.BusinessAdmin))
                throw new VmsException(Codes.EmailInUse, "The email supplied is in use.");

            if (!await _businessRepository.ContainsBusinessAsync(businessId))
            {
                //TODO: consider calling business service as back-up to avoid data inconsistency.
                throw new VmsException(Codes.BusinessNotFound,
                    "The business cannot be found to create the account for.");
            }

            var pending = new PendingIdentity(Guid.NewGuid(), email, Roles.BusinessAdmin, businessId);

            await _pendingIdentityRepository.AddAsync(pending);

            _logger.LogInformation($"Pending identity for business admin created with email: {email} and code: {pending.Id}.");

            _serviceBus.PublishEvent(new PendingBusinessAdminCreated(pending.Id, pending.Email), RequestInfo.Empty);
        }

        public async Task DeleteBusinessAdmin(Guid id, Guid businessId)
        {
            var identity = await _identityRepository.GetAsync(id, businessId);
            if (identity != null)
            {
                await _identityRepository.RemoveAsync(identity);
                return;
            }

            var pending = await _pendingIdentityRepository.GetAsync(id, businessId);
            if (pending != null)
            {
                await _pendingIdentityRepository.RemoveAsync(pending);
                return;
            }

            _logger.LogWarning($"Admin with id: {id} could not be found to be deleted.");
            throw new VmsException(Codes.NoIdentityFound, "The admin could not be found to be deleted.");
        }
    }
}
