using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Convey.HTTP;
using Services.Users.Services.Dtos;

namespace Services.Users.Services
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly IHttpClient _client;
        private readonly string _siteAddress;
        private readonly string _businessAddress;

        public ServicesRepository(IHttpClient client, HttpClientOptions options)
        {
            _client = client;
            _siteAddress = options.Services["site"];
            _businessAddress = options.Services["business"];
        }


        public async Task<bool> IsSiteIdValid(Guid siteId)
        {
            var site = await _client.GetAsync<SiteDto>($"{_siteAddress}/{siteId}");
            return site != null;
        }

        public async Task<bool> IsBusinessIdValid(Guid businessId)
        {
            var business = await _client.GetAsync<BusinessDto>($"{_businessAddress}/{businessId}");
            return business != null;
        }
    }
}
