using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Services.Identity.Domain;
using Services.Identity.Repositorys;
using Services.RabbitMq.Interfaces.Wrappers;

namespace Services.Identity.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _repository;

        public RefreshTokenService(IRefreshTokenRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> CreateRefreshToken(string email)
        {
            using var randomGenerator = RandomNumberGenerator.Create();
            byte[] tokenBytes = new byte[256/8];
            randomGenerator.GetBytes(tokenBytes);
            var token = Convert.ToBase64String(tokenBytes);
            await _repository.AddAsync(new RefreshToken(token, email));
            return token;
        }
    }
}
