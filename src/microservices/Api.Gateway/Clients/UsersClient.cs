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

        public Task<IEnumerable<SiteAccessDetailsDto>> GetBusinessAccessRecordsAsync(Guid businessId)
        {
            return _client.GetAsync<IEnumerable<SiteAccessDetailsDto>>($"{_baseAddress}/business-records/{businessId}");   
        }

        public Task<UserInfoDto> GetUserInfo(Guid accountId)
        {
            return _client.GetAsync<UserInfoDto>($"{_baseAddress}/info/{accountId}");
        }

        public Task<IEnumerable<UserSnapshotDto>> GetUsersForBusiness(Guid businessId) 
            => _client.GetAsync<IEnumerable<UserSnapshotDto>>($"{_baseAddress}/users/{businessId}");

        public Task<IEnumerable<AccessRecordDto>> GetAccessRecordForUserAsyncById(Guid userId)
        {
            return _client.GetAsync<IEnumerable<AccessRecordDto>>($"{_baseAddress}/admin-records/{userId}");
        }

        public Task<IEnumerable<LatestAccessRecordDto>> GetLatestStateForSite(Guid siteId)
        {
            return  _client.GetAsync<IEnumerable<LatestAccessRecordDto>>($"{_baseAddress}/site-availability/{siteId}");
        }
    }
}
