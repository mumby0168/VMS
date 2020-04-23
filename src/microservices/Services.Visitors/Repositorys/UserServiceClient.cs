using System;
using System.Net;
using System.Threading.Tasks;
using Convey.HTTP;
using Services.Common.Logging;
using Services.Visitors.Dtos;

namespace Services.Visitors.Repositorys
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly IVmsLogger<UserServiceClient> _vmsLogger;
        private readonly IHttpClient _client;
        private readonly string _baseAddress;

        public UserServiceClient(IVmsLogger<UserServiceClient> vmsLogger,IHttpClient client, HttpClientOptions options)
        {
            _baseAddress = options.Services["users"];
            _vmsLogger = vmsLogger;
            _client = client;
        }

        public async Task<bool> ContainsUserAsync(Guid userId)
        {
            var user = await _client.GetAsync($"{_baseAddress}/rest/{userId}");
            _vmsLogger.LogInformation($"Checked user service for id: {userId} got response {user.StatusCode}");
            return user.StatusCode == HttpStatusCode.OK;
        }

        public async Task<UserDto> GetUserAsync(Guid visitingId)
        {
            var user = await _client.GetAsync<UserDto>($"{_baseAddress}/rest/{visitingId}");
            return user;
        }
    }
}