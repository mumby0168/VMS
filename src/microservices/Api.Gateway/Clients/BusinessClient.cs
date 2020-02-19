using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Clients.Interfaces;
using Api.Gateway.Dtos.Business;
using Convey.HTTP;

namespace Api.Gateway.Clients
{
    public class BusinessClient : IBusinessClient
    {        
        private readonly IHttpClient _client;
        private readonly string _baseAddress;

        public BusinessClient(IHttpClient client, HttpClientOptions options)
        {
            _baseAddress = options.Services["business"];
            _client = client;
        }
        public Task<IEnumerable<BusinessSummaryDto>> GetBusinessSummaries() 
            => _client.GetAsync<IEnumerable<BusinessSummaryDto>>($"{_baseAddress}");
        public Task<BusinessDto> GetBusiness(Guid id) => _client.GetAsync<BusinessDto>($"{_baseAddress}/{id}");
    }
}
