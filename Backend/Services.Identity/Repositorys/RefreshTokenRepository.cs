using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Common.Exceptions;
using Services.Common.Mongo;
using Services.Identity.Domain;

namespace Services.Identity.Repositorys
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoRepository<RefreshToken> _repository;
        private readonly ILogger<RefreshTokenRepository> _logger;

        public RefreshTokenRepository(IMongoRepository<RefreshToken> repository, ILogger<RefreshTokenRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task AddAsync(RefreshToken refreshToken) => _repository.AddAsync(refreshToken);

        public async Task RemoveAsync(string token, string email)
        {
            var refreshToken = await _repository.GetAsync(t => t.Email == email && t.Token == token);
            if (refreshToken is null)
            {
                _logger.LogWarning($"Token did not exist on removal from user: {email} ");
                throw new VmsException(Codes.NoRefreshToken, "The refresh token was not found to be revoked.");
            }

            await _repository.RemoveAsync(refreshToken.Id);
        }
    }
}
