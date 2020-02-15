using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Jwt;
using Services.Common.Logging;
using Services.Identity.Managers;
using Services.Identity.Models;
using Services.Identity.Repositorys;

namespace Services.Identity.Services
{
    public class GreetingSystemService : IGreetingSystemService
    {
        private readonly IVmsLogger<GreetingSystemService> _logger;
        private readonly IPasswordManager _passwordManager;
        private readonly IIdentityRepository _identityRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IJwtManager _jwtManager;
        private readonly IRefreshTokenService _tokenService;

        public GreetingSystemService(IVmsLogger<GreetingSystemService> logger, IPasswordManager passwordManager, IIdentityRepository identityRepository, IBusinessRepository businessRepository, IJwtManager jwtManager, IRefreshTokenService tokenService)
        {
            _logger = logger;
            _passwordManager = passwordManager;
            _identityRepository = identityRepository;
            _businessRepository = businessRepository;
            _jwtManager = jwtManager;
            _tokenService = tokenService;
        }
        public async Task<IAuthToken> SignIn(string email, string password, int businessCode)
        {
            var identity = await _identityRepository.GetByEmail(email);
            if (identity is null || identity.Role == Roles.SystemAdmin)
            {
                _logger.LogWarning($"No user found with email: {email} attempting to log into greeting system.");
                throw new VmsException(Codes.InvalidCredentials, "The credentials provided where incorrect.");
            }

            if(!await _businessRepository.IsCodeValidAsync(businessCode))
            {
                _logger.LogInformation($"No business found with code {businessCode}.");
                throw new VmsException(Codes.InvalidCredentials, "The credentials provided where incorrect.");
            }

            if (!_passwordManager.IsPasswordCorrect(password, identity.Hash, identity.Salt))
            {
                _logger.LogWarning($"Incorrect password for: {email}");
                throw new VmsException(Codes.InvalidCredentials, "The credentials provided where incorrect.");
            }

            var jwt = _jwtManager.CreateToken(Guid.NewGuid(), identity.Email, Roles.Greeting);
            //var refresh = await _tokenService.CreateRefreshToken(id)
            //TODO: Add a mechanism to store more that just email here unique id needs to be stored instead.

            return AuthToken.Create(jwt, "");

        }
    }
}
