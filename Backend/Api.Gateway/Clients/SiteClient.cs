using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Clients.Interfaces;
using Api.Gateway.Dtos.Sites;
using Convey.HTTP;

namespace Api.Gateway.Clients
{
    public class SiteClient : ISiteClient
    {
        private readonly IHttpClient _client;
        private readonly string _baseAddress;

        public SiteClient(IHttpClient client, HttpClientOptions options)
        {
            _baseAddress = options.Services["site"];
            _client = client;
        }

        public Task<IEnumerable<SiteSummaryDto>> GetSites(Guid businessId) =>
            _client.GetAsync<IEnumerable<SiteSummaryDto>>($"{_baseAddress}/summaries/{businessId.ToString()}");

        public Task<SiteDto> GetSite(Guid siteId) =>
            _client.GetAsync<SiteDto>($"{_baseAddress}/{siteId.ToString()}");

        public Task<IEnumerable<SiteResourceDto>> GetResourcesForSite(Guid siteId) =>
            _client.GetAsync<IEnumerable<SiteResourceDto>>($"{_baseAddress}/resources/{siteId.ToString()}");

    }
}
