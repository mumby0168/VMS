using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.HTTP;
using Services.Sites.Dtos;

namespace Services.Sites.Services
{
    public class SystemServices : ISystemServices
    {
        private readonly IHttpClient _client;
        private readonly string _baseAddress;

        public SystemServices(IHttpClient client, HttpClientOptions options)
        {
            _baseAddress = options.Services["users"];
            _client = client;
        }
        public Task<IEnumerable<LatestAccessRecordDto>> GetLatestAccessRecordsForSite(Guid siteId)
        {
            return _client.GetAsync<IEnumerable<LatestAccessRecordDto>>($"{_baseAddress}/site-availability/{siteId}");
        }
    }
}
