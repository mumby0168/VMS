using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Clients.Interfaces;
using Api.Gateway.Dtos.Users;
using Convey.HTTP;

namespace Api.Gateway.Clients
{
    public class UsersClient : IUsersClient
    {
        private readonly IHttpClient _client;
        private readonly string _baseAddress;

        public UsersClient(IHttpClient client, HttpClientOptions options)
        {
            _baseAddress = options.Services["users"];
            _client = client;
        }
        public Task<IEnumerable<AccessRecordDto>> GetAccessRecordForUserAsync(Guid accountId) =>
            _client.GetAsync<IEnumerable<AccessRecordDto>>($"{_baseAddress}/records/{accountId}");
    }
}
