using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Exceptions;
using Services.Common.Jwt;
using Services.Identity.Domain;
using Services.Identity.Managers;
using Services.Identity.Messages.Events;
using Services.Identity.Models;
using Services.Identity.Repositorys;
using Services.RabbitMq.Messages;

namespace Services.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IServiceBusMessagePublisher _serviceBus;
        private readonly IIdentityRepository _identityRepository;
        private readonly IPendingIdentityRepository _pendingIdentityRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly IJwtManager _jwtManager;
        private readonly ILogger<IdentityService> _logger;
        private readonly IRefreshTokenService _tokenService;

        public IdentityService(IServiceBusMessagePublisher serviceBus, IIdentityRepository identityRepository, IPendingIdentityRepository pendingIdentityRepository, IPasswordManager passwordManager, IJwtManager jwtManager, ILogger<IdentityService> logger, IRefreshTokenService tokenService)
        {
            _serviceBus = serviceBus;
            _identityRepository = identityRepository;
            _pendingIdentityRepository = pendingIdentityRepository;
            _passwordManager = passwordManager;
            _jwtManager = jwtManager;
            _logger = logger;
            _tokenService = tokenService;
        }
        public async Task<IAuthToken> SignIn(string email, string password, string role)
        {
            var identity = await _identityRepository.GetByEmailAndRole(email, role);
            if (identity is null) 
                throw new VmsException(Codes.InvalidCredentials, "The credentials provided where incorrect.");

            if(!_passwordManager.IsPasswordCorrect(password, identity.Hash, identity.Salt)) 
                throw new VmsException(Codes.InvalidCredentials, "The credentials provided where incorrect.");

            var jwt = _jwtManager.CreateToken(identity.Id, identity.Email, identity.Role);
            var refreshToken = await _tokenService.CreateRefreshToken(identity.Email);

            return AuthToken.Create(jwt, refreshToken);
        }

        public async Task CreateAdmin(string email)
        {
            if(await _identityRepository.IsEmailInUse(email) || await _pendingIdentityRepository.IsEmailInUse(email))
                throw new VmsException(Codes.EmailInUse, "The email supplied is already in use.");

            var pending = new PendingIdentity(Guid.NewGuid(), email);

            await _pendingIdentityRepository.AddAsync(pending);

            _logger.LogInformation($"create code issued: {pending.Id}.");

            _serviceBus.PublishEvent(new PendingAdminCreated(pending.Id, pending.Email), RequestInfo.Empty);
        }

        public async Task CompleteAdmin(Guid code, string password, string passwordMatch, string email)
        {
            var pendingIdentity = await _pendingIdentityRepository.GetAsync(code, email);
            if(pendingIdentity is null) throw new VmsException(Codes.InvalidCredentials, "The credentials are invalid.");
            if(password != passwordMatch) throw new VmsException(Codes.InvalidCredentials, "The credentials are invalid.");

            var pword = _passwordManager.EncryptPassword(password);
            var identity = new Domain.Identity(email, pword.Hash, pword.Salt, Roles.SystemAdmin);

            await _identityRepository.AddAsync(identity);
        }
    }
}
