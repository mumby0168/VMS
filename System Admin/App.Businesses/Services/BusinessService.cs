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

namespace App.Businesses.Services
{
    public class BusinessService
    {
        private const string BaseBusinessesAddress = "http://localhost:5020/gateway/api/businesses/";
        
        private readonly ILogger<BusinessService> _logger;

        public HttpClient Client { get; }
        public BusinessService(HttpClient client, IUserContext context, ILogger<BusinessService> logger)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {context.Token}");
            client.BaseAddress = new Uri(BaseBusinessesAddress);
            Client = client;
            _logger = logger;
        }

        /// <summary>
        /// Creates a business by making a remote request.
        /// </summary>
        /// <param name="business"></param>
        /// <returns>Operation Id</returns>
        public async Task<Guid> CreateBusiness(Business business)
        {
            var message = new StringContent(
                JsonConvert.SerializeObject(new { Name = business.Name, TradingName = business.TradingName, WebAddress = business.WebAddress,
                    HeadContactFirstName = business.Contact.FirstName, HeadContactSecondName = business.Contact.SecondName, HeadContactContactNumber = business.Contact.ContactNumber, HeadContactEmail = business.Contact.Email,
                    HeadOfficePostCode = business.Office.PostCode, HeadOfficeAddressLine1 = business.Office.AddressLine1 , HeadOfficeAddressLine2 = business.Office.AddressLine2})
                , Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await Client.PostAsync("/create", message);
            }
            catch(HttpRequestException e)
            {
                _logger.LogError("The request failed to create a business: " + e.Message);
                throw new InternalHttpRequestException(e);
            }

            return response.GetOperationId();
        }
    }
}