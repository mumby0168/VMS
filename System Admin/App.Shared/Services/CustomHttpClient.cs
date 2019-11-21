using App.Shared.Context;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace App.Shared.Services
{
    public class CustomHttpClient : IHttpClient
    {

        private const string BaseUrl = @"http://localhost:5020/gateway/api/";
        private const string BaseIdentityUrl = @"http://localhost:5010/api/";

        public CustomHttpClient(IHttpClientFactory factory, IUserContext context)
        {
            var baseClient = factory.CreateClient();
            var identityClient = factory.CreateClient();
            baseClient.BaseAddress = new Uri(BaseUrl);
            identityClient.BaseAddress = new Uri(BaseIdentityUrl);

            string auth = $"Bearer {context.Token}";

            baseClient.DefaultRequestHeaders.Add("Authorization", auth);
            identityClient.DefaultRequestHeaders.Add("Authorization", auth);

            GatewayClient = baseClient;
            IdentityClient = identityClient;
        }


        public HttpClient GatewayClient { get; }

        public HttpClient IdentityClient { get; }
    }
}
