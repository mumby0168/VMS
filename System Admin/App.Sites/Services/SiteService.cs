using App.Shared.Context;
using App.Shared.Http;
using App.Shared.Services;
using App.Sites.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Sites.Services
{
    public class SiteService
    {
        private readonly IHttpExecutor _executor;
        private const string BaseAddress = @"http://localhost:5020/gateway/api/sites/";

        public HttpClient Client { get; }

        public SiteService(HttpClient client, IHttpExecutor executor, IUserContext context)
        {
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {context.Token}");
            _executor = executor;
            Client = client;            
        }

        public Task<bool> CreateSiteAsync(Site site)
        =>
            _executor.SendRequestAsync(() => Client.PostAsync("create",
            JsonMessage.CreateJsonMessage(
                new { site.Name, site.BusinessId, site.AddressLine1,site.PostCode, site.AddressLine2, site.Email, site.ContactNumber, site.FirstName, site.SecondName}
                )),
            $"{site.Name} site created succesfully.");        
    }
}
