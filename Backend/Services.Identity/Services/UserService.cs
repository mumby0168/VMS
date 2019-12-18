using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Jwt;
using Services.Common.Logging;
using Services.Identity.Managers;
using Services.Identity.Messages.Commands;
using Services.Identity.Messages.Events;
using Services.Identity.Repositorys;
using Services.RabbitMq.Messages;

namespace Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IPendingIdentityRepository _pendingIdentityRepository;
        private readonly IIdentityRepository _repository;
        private readonly IVmsLogger<UserService> _logger;
        private readonly IPasswordManager _passwordManager;
        private readonly IServiceBusMessagePublisher _publisher;

        public UserService(IPendingIdentityRepository pendingIdentityRepository, IIdentityRepository repository, IVmsLogger<UserService> logger, IPasswordManager passwordManager, IServiceBusMessagePublisher publisher)
        {
            _pendingIdentityRepository = pendingIdentityRepository;
            _repository = repository;
            _logger = logger;
            _passwordManager = passwordManager;
            _publisher = publisher;
        }
        public async Task CompleteUser(Guid code, string email, string password, string passwordConfirm)
        {
            var pending = await _pendingIdentityRepository.GetAsync(code, email);
            if (pending is null)
            {
                _logger.LogWarning($"Pending user not found with code: {code} and email: {email}");
                throw new VmsException(Codes.InvalidCredentials, "The account registration has not been made.");
            }


            //TODO: make sure this check is done on creation of account pending. 
            //var existing = await _repository.GetByEmailAndRole(email, Roles.);
            //if (existing != null)
            //    throw new VmsException(Codes.EmailInUse, "Their has already been an account created with this email.");

            if (password != passwordConfirm)
                throw new VmsException(Codes.InvalidCredentials, "The credentials are invalid.");

            var pword = _passwordManager.EncryptPassword(password);
            var identity = new Domain.Identity(email, pword.Hash, pword.Salt, pending.Role, pending.BusinessId);

            await _repository.AddAsync(identity);
            await _pendingIdentityRepository.RemoveAsync(pending);
            _publisher.PublishEvent(new UserAccountCreated(identity.Id, identity.Email), RequestInfo.Empty);
        }
    }
}
