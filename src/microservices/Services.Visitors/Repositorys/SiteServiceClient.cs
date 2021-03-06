using System;
using System.Threading.Tasks;
using Convey.HTTP;
using Services.Visitors.Dtos;

namespace Services.Visitors.Repositorys
{
    public class SiteServiceClient : ISiteServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _baseAddress;
        public SiteServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _baseAddress = options.Services["site"];
            _httpClient = httpClient;
        }
        
        public Task<SiteDto> GetSiteAsync(Guid siteId)
        {
            return _httpClient.GetAsync<SiteDto>($"{_baseAddress}/rest/{siteId}");
        }
    }
}