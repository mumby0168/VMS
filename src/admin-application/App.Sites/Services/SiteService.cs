using App.Shared.Context;
using App.Shared.Http;
using App.Shared.Services;
using App.Sites.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Sites.Services
{
    public class SiteService
    {
        private readonly IHttpExecutor _executor;
        private readonly ILogger<SiteService> _logger;
        private readonly string _baseAddress;

        public HttpClient Client { get; }

        public SiteService(HttpClient client, IHttpExecutor executor, IUserContext context, ILogger<SiteService> logger, Endpoints endpoints)
        {
            _baseAddress = endpoints.Gateway + "sites/";
            client.BaseAddress = new Uri(_baseAddress);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {context.Token}");
            _executor = executor;
            _logger = logger;
            Client = client;            
        }

        public Task<bool> CreateSiteAsync(Site site)
        =>
            _executor.SendRequestAsync(() => Client.PostAsync("create",
            JsonMessage.CreateJsonMessage(
                new { site.Name, site.BusinessId, site.AddressLine1,site.PostCode, site.AddressLine2, site.Email, site.ContactNumber, site.FirstName, site.SecondName}
                )),
            $"{site.Name} site created succesfully.");

        public Task<bool> UpdateSiteDetailsAsync(Guid siteId, string name, string addressLine1, string addressLine2, string postCode) => _executor.SendRequestAsync(() => Client.PostAsync("update", JsonMessage.CreateJsonMessage(new { siteId, name, addressLine1, addressLine2, postCode })), "Site details updated succesfully.");


        public Task<bool> UpdateSiteContactAsync(Guid siteId, string firstName, string secondName, string email, string contactNumber) => _executor.SendRequestAsync(() => Client.PostAsync("update-contact", JsonMessage.CreateJsonMessage(new { siteId, firstName, secondName, email, contactNumber })), "Site contact details updated succesfully.");


        public Task<bool> CreateSiteResource(Guid siteId, string name, string identifier) => _executor.SendRequestAsync(() => Client.PostAsync("create-site-resource",JsonMessage.CreateJsonMessage(new { siteId, name, identifier })), "The resource was added succesfully.");

        public Task<bool> RemoveSiteResourceAsync(Guid resourceId) => _executor.SendRequestAsync(() => Client.PostAsync("remove-site-resource", JsonMessage.CreateJsonMessage(new { resourceId})), "Resource removed succesfully.");

        public Task<IEnumerable<SiteResource>> GetResourcesForSite(Guid siteId) => _executor.GetAsync<IEnumerable<SiteResource>>(_baseAddress + $"resources/{siteId}");

        public Task<IEnumerable<SiteSummary>> GetSiteSummariesForBusiness(Guid businessId) => _executor.GetAsync<IEnumerable<SiteSummary>>(_baseAddress + $"summaries/{businessId}");

        public Task<Site> GetSite(Guid siteId) => _executor.GetAsync<Site>(_baseAddress + $"get/{siteId}");                    
    }
}
