using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Clients.Interfaces;
using Api.Gateway.Dtos.Visitors;
using Convey.HTTP;

namespace Api.Gateway.Clients
{
    public class VisitorClient : IVisitorsClient
    {
        private readonly IHttpClient _client;
        private readonly string _baseAddress;

        public VisitorClient(IHttpClient client, HttpClientOptions options)
        {
            _baseAddress = options.Services["visitors"];
            _client = client;
        }

        public Task<IEnumerable<DataSpecificationDto>> GetDataSpecificationsForBusinessAsync(Guid businessId)
            => _client.GetAsync<IEnumerable<DataSpecificationDto>>($"{_baseAddress}/spec/entries/{businessId}");

        public Task<IEnumerable<string>> GetDataSpecificationValidatorsAsync() =>
            _client.GetAsync<IEnumerable<string>>($"{_baseAddress}/spec/validators/");

        public Task<IEnumerable<VisitorDto>> GetVisitorsForSiteAsync(Guid siteId) =>
            _client.GetAsync<IEnumerable<VisitorDto>>($"{_baseAddress}/site/{siteId}");

        public Task<IEnumerable<VisitorInformationDto>> GetDataForVisitorAsync(Guid visitorId)
            => _client.GetAsync<IEnumerable<VisitorInformationDto>>($"{_baseAddress}/info/{visitorId}");

        public Task<IEnumerable<VisitorRecordDto>> GetVisitorsForBusinessAsync(Guid businessId) =>
            _client.GetAsync<IEnumerable<VisitorRecordDto>>($"{_baseAddress}/business/{businessId}");
    }
}
