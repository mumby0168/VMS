using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Jwt;
using Services.Identity.Managers;
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

        public IdentityService(IServiceBusMessagePublisher serviceBus, IIdentityRepository identityRepository, IPendingIdentityRepository pendingIdentityRepository, IPasswordManager passwordManager, IJwtManager jwtManager)
        {
            _serviceBus = serviceBus;
            _identityRepository = identityRepository;
            _pendingIdentityRepository = pendingIdentityRepository;
            _passwordManager = passwordManager;
            _jwtManager = jwtManager;
        }
        public async Task<string> SignIn(string email, string password, string role)
        {
            var identity = await _identityRepository.GetByEmailAndRole(email, role);
            if (identity is null) 
                throw new VmsException(Codes.InvalidCredentials, "The credentials provided where incorrect.");

            if(!_passwordManager.IsPasswordCorrect(password, identity.Hash, identity.Salt)) 
                throw new VmsException(Codes.InvalidCredentials, "The credentials provided where incorrect.");

            return _jwtManager.CreateToken(identity.Id, identity.Email, identity.Role);
        }

        public async Task CreateAdmin(string email)
        {
            if(await _identityRepository.IsEmailInUse(email) || await _pendingIdentityRepository.IsEmailInUse(email))
                throw new VmsException(Codes.EmailInUse, "The email supplied is already in use.");


        }

        public Task CompleteAdmin(Guid code, string password, string passwordMatch, string email)
        {
            throw new NotImplementedException();
        }
    }
}
