using System;
using System.Net;
using System.Threading.Tasks;
using Convey.HTTP;
using Services.Common.Logging;

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
            var site = await _client.GetAsync($"{_baseAddress}/rest/{userId}");
            _vmsLogger.LogInformation($"Checked user service for id: {userId} got response {site.StatusCode}");
            return site.StatusCode == HttpStatusCode.OK;
        }
    }
}