using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Clients.Interfaces;
using Api.Gateway.Dtos.Operations;
using Convey.HTTP;

namespace Api.Gateway.Clients
{
    public class OperationsClient : IOperationsClient
    {
        private readonly IHttpClient _client;
        private readonly string _address;

        public OperationsClient(IHttpClient client, HttpClientOptions options)
        {
            _client = client;
            _address = options.Services["operations"];
        }

        public Task<OperationDto> GetAsync(Guid id) => _client.GetAsync<OperationDto>($"{_address}/{id.ToString()}");
    }
}
