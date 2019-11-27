using App.Shared.Context;
using App.Shared.Exceptions;
using App.Shared.Http;
using App.Shared.Services;
using App.Sites.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private readonly ILogger<SiteService> _logger;
        private const string BaseAddress = @"http://localhost:5020/gateway/api/sites/";

        public HttpClient Client { get; }

        public SiteService(HttpClient client, IHttpExecutor executor, IUserContext context, ILogger<SiteService> logger)
        {
            client.BaseAddress = new Uri(BaseAddress);
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


        public async Task<IEnumerable<SiteSummary>> GetSiteSummariesForBusiness(Guid businessId)
        {
            try
            {
                var response = await Client.GetAsync($"summaries/{businessId}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<SiteSummary>>(await response.Content.ReadAsStringAsync());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return null;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("The request failed to get business summaries: " + e.Message);
                throw new InternalHttpRequestException(e);
            }

            throw new NotImplementedException("This should do something to offer a reload of the data.");
        }

        public async Task<Site> GetSite(Guid siteId)
        {
            try
            {
                var response = await Client.GetAsync($"get/{siteId}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<Site>(await response.Content.ReadAsStringAsync());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return null;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("The request failed to get business summaries: " + e.Message);
                throw new InternalHttpRequestException(e);
            }

            throw new NotImplementedException("This should do something to offer a reload of the data.");
        }
    }
}
