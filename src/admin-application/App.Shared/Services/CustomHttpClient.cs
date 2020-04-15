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

        private readonly HttpClient _baseClient; 
        private readonly HttpClient _identityClient; 

        private readonly IUserContext _userContext;
        public CustomHttpClient(IHttpClientFactory factory, IUserContext context, Endpoints endpoints)
        {
            var baseClient = factory.CreateClient();
            var identityClient = factory.CreateClient();
            baseClient.BaseAddress = new Uri(endpoints.Gateway);
            identityClient.BaseAddress = new Uri(endpoints.Identity);            

            _baseClient = baseClient;
            _identityClient = identityClient;
            _userContext = context;
        }


        public HttpClient GatewayClient  
        {
            get {
                string auth = $"Bearer {_userContext.Token}";

                _baseClient.DefaultRequestHeaders.Add("Authorization", auth);                

                return _baseClient;
            }
        }

        public HttpClient IdentityClient
        {
            get {
                string auth = $"Bearer {_userContext.Token}";
                
                _identityClient.DefaultRequestHeaders.Add("Authorization", auth);

                return _identityClient;
            }
        }

    }
}
