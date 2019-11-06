using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Gateway.HttpServices
{
    public class OperationsService
    {
        public HttpClient HttpClient { get; }

        public OperationsService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
