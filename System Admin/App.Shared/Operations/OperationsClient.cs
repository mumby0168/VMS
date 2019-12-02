using App.Shared.Exceptions;
using App.Shared.Http;
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
        private readonly ILogger<OperationsClient> _logger;        

        public HttpClient Client { get; } 
        public OperationsClient(HttpClient client, ILogger<OperationsClient> logger, Endpoints endpoints)
        {
            client.BaseAddress = new Uri(endpoints.Gateway + "operations/");
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
                if (result.State == OperationStatus.Complete.ToString().ToLower()) return new OperationMessage(result.Id, OperationStatus.Complete);
                else if (result.State == OperationStatus.Failed.ToString().ToLower()) return new OperationMessageFailed(result.Id, OperationStatus.Failed, result.Code, result.Reason);
            }
            return null;
        }

        private class OperationResponse 
        {
            public Guid Id { get; set; }
                
            public string State { get; set; }

            public string Code { get; set; }

            public string Reason { get; set; }
        }
    }
}
