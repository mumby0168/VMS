using App.Shared.Context;
using App.Shared.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace App.Shared.Services
{
    public class CustomHttpClient : IHttpClient
    {       
        public CustomHttpClient(IHttpClientFactory factory, IUserContext context, Endpoints endpoints)
        {
            var baseClient = factory.CreateClient();
            var identityClient = factory.CreateClient();
            baseClient.BaseAddress = new Uri(endpoints.Gateway);
            identityClient.BaseAddress = new Uri(endpoints.Identity);

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
