using App.Shared.Exceptions;
using App.Shared.Operations.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Operations
{
    public class OperationsClient : IOperationsClient
    {
        private const string BaseAddress = "http://localhost:5020/gateway/api/operations/";
        private readonly ILogger<OperationsClient> _logger;        

        public HttpClient Client { get; } 
        public OperationsClient(HttpClient client, ILogger<OperationsClient> logger)
        {
            client.BaseAddress = new Uri(BaseAddress);
            Client = client;
            _logger = logger;
        }        

        public async Task<IOperationMessage> GetOperationMessageAsync(Guid id)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await Client.GetAsync($"{id.ToString()}");
            }
            catch (HttpRequestException e)
            {
                _logger.LogWarning("Request failed with error: " + e.Message);
                throw new InternalHttpRequestException(e);
            }

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _logger.LogInformation("Got operation through http request");
                //TODO: inject this so can test.
                var result = JsonConvert.DeserializeObject<OperationResponse>(await response.Content.ReadAsStringAsync());
                if (result.Status == OperationStatus.Complete.ToString()) return new OperationMessage(result.Id, OperationStatus.Complete);
                else if (result.Status == OperationStatus.Failed.ToString()) return new OperationMessageFailed(result.Id, OperationStatus.Failed, result.Code, result.Reason);
            }
            return null;
        }

        private class OperationResponse 
        {
            public Guid Id { get; set; }
                
            public string Status { get; set; }

            public string Code { get; set; }

            public string Reason { get; set; }
        }
    }
}
