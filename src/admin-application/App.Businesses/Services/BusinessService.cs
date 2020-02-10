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
using App.Shared.Http;

namespace App.Businesses.Services
{
    public class BusinessService
    {
        private readonly string _baseBusinessesAddress;                
        private readonly IHttpExecutor _httpExecutor;

        public HttpClient Client { get; }
        public BusinessService(IUserContext context, IHttpExecutor httpExecutor, Endpoints endpoints, HttpClient client)
        {            
            _baseBusinessesAddress = endpoints.Gateway + "businesses/";
            client.BaseAddress = new Uri(_baseBusinessesAddress);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {context.Token}");
            Client = client;            
            _httpExecutor = httpExecutor;
        }

        public Task<bool> UpdateContactAsync(HeadContact contact, Guid businessId) => 
            _httpExecutor.SendRequestAsync(() => Client.PostAsync("update-contact", 
                JsonMessage.CreateJsonMessage(
                    new { BusinessId = businessId, FirstName = contact.FirstName, SecondName = contact.SecondName, Email = contact.Email, ContactNumber = contact.ContactNumber })),
                "Business contact updated succesfully.");        

        public Task<bool> UpdateOfficeAsync(HeadOffice office, Guid businessId) =>
            _httpExecutor.SendRequestAsync(() => Client.PostAsync("update-office", JsonMessage.CreateJsonMessage(new { BusinessId = businessId, office.PostCode, office.AddressLine1, office.AddressLine2 })), "Business contact updated succesfully");

        public Task<bool> UpdateDetails(Guid id, string name, string tradingName, string webAddress) 
            => _httpExecutor.SendRequestAsync(() => Client.PostAsync("update-details", JsonMessage.CreateJsonMessage(new { id, name, tradingName, webAddress })), "Business details updated successfully");

        public Task<bool> CreateBusiness(Business business)
        {
            var message = JsonMessage.CreateJsonMessage(
                new { Name = business.Name, TradingName = business.TradingName, WebAddress = business.WebAddress,
                    HeadContactFirstName = business.Contact.FirstName, HeadContactSecondName = business.Contact.SecondName, HeadContactContactNumber = business.Contact.ContactNumber, HeadContactEmail = business.Contact.Email,
                    HeadOfficePostCode = business.Office.PostCode, HeadOfficeAddressLine1 = business.Office.AddressLine1 , HeadOfficeAddressLine2 = business.Office.AddressLine2});

            return _httpExecutor.SendRequestAsync(() => Client.PostAsync("create", message), $"{business.Name} account created succesfully");
        }

        public Task<IEnumerable<BusinessSummary>> GetBusinessSummariesAsync() 
            => _httpExecutor.GetAsync<IEnumerable<BusinessSummary>>(_baseBusinessesAddress);

        public Task<Business> GetBusiness(Guid id) => _httpExecutor.GetAsync<Business>(_baseBusinessesAddress + id.ToString());
       
    }
}