using System.Net.Http;
using System;
using App.Shared.Context;
using App.Businesses.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using App.Shared.Exceptions;
using App.Shared.Extensions;
using System.Collections.Generic;
using App.Shared.Services;

namespace App.Businesses.Services
{
    public class BusinessService
    {
        private const string BaseBusinessesAddress = "http://localhost:5020/gateway/api/businesses/";
        
        private readonly ILogger<BusinessService> _logger;
        private readonly IHttpExecutor _httpExecutor;

        public HttpClient Client { get; }
        public BusinessService(HttpClient client, IUserContext context, ILogger<BusinessService> logger, IHttpExecutor httpExecutor)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {context.Token}");
            client.BaseAddress = new Uri(BaseBusinessesAddress);
            Client = client;
            _logger = logger;
            _httpExecutor = httpExecutor;
        }

        public async Task<Guid> UpdateContactAsync(HeadContact contact, Guid businessId)
        {
            var message = new StringContent(JsonConvert.SerializeObject(new { BusinessId = businessId, FirstName = contact.FirstName, SecondName = contact.SecondName, Email = contact.Email, ContactNumber = contact.ContactNumber }), Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await Client.PostAsync("update-contact", message);
                if(response.IsSuccessStatusCode)
                {
                    return response.GetOperationId();
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("The request failed to update the business contact business: " + e.Message);
                throw new InternalHttpRequestException(e);
            }
            throw new NotImplementedException("Not sure what to do here yet");
        }

        /// <summary>
        /// Creates a business by making a remote request.
        /// </summary>
        /// <param name="business"></param>
        /// <returns>Operation Id</returns>
        public Task<bool> CreateBusiness(Business business)
        {
            var message = new StringContent(
                JsonConvert.SerializeObject(new { Name = business.Name, TradingName = business.TradingName, WebAddress = business.WebAddress,
                    HeadContactFirstName = business.Contact.FirstName, HeadContactSecondName = business.Contact.SecondName, HeadContactContactNumber = business.Contact.ContactNumber, HeadContactEmail = business.Contact.Email,
                    HeadOfficePostCode = business.Office.PostCode, HeadOfficeAddressLine1 = business.Office.AddressLine1 , HeadOfficeAddressLine2 = business.Office.AddressLine2})
                , Encoding.UTF8, "application/json");

            return _httpExecutor.SendRequestAsync(() => Client.PostAsync("create", message), $"{business.Name} account created succesfully");
        }

        public async Task<IEnumerable<BusinessSummary>> GetBusinessSummariesAsync()
        {            
            try
            {
                var response = await Client.GetAsync("");
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<BusinessSummary>>(await response.Content.ReadAsStringAsync());
                }                
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("The request failed to get business summaries: " + e.Message);
                throw new InternalHttpRequestException(e);                
            }

            throw new NotImplementedException("This should do something to offer a reload of the data.");
        }

        public async Task<Business> GetBusiness(Guid id)
        {
            try
            {
                var response = await Client.GetAsync(id.ToString());
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Business>(json);
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"The request failed to get business with id: {id} error: " + e.Message);
                throw new InternalHttpRequestException(e);
                throw;
            }
            throw new NotImplementedException("This should do something to offer a reload of the data.");
        }
    }
}