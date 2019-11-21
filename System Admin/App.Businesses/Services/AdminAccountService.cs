using App.Businesses.Models;
using App.Shared.Exceptions;
using App.Shared.Http;
using App.Shared.Models;
using App.Shared.Services;
using Blazored.Toast.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Businesses.Services
{
    public class AdminAccountService : IAdminAccountService
    {
        private readonly ILogger<AdminAccountService> _logger;
        private readonly IHttpClient _client;
        private readonly IToastService _toastService;

        public AdminAccountService(ILogger<AdminAccountService> logger, IHttpClient client, IToastService toastService)
        {
            _logger = logger;
            _client = client;
            _toastService = toastService;
        }

        public async Task<bool> DeleteAccount(Guid accountId, Guid businessId)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await _client.IdentityClient.PostAsync("admin/remove", JsonMessage.CreateJsonMessage(new { BusinessId = businessId, Id = accountId })); ;
            }
            catch (HttpRequestException)
            {
                _toastService.ShowError(ServiceError.Standard.Reason);
                return false;
            }

            if (response.IsSuccessStatusCode)
            {                
                _toastService.ShowSuccess($"admin removed successfully.");
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var error = await ServiceError.Deserialize(response);
                _toastService.ShowError(error.Reason);
                return false;
            }

            return false;
        }

        public async Task<IEnumerable<AccountInfo>> GetAccountsForBusiness(Guid businessId)
        {
            try
            {
                var response = await _client.IdentityClient.GetAsync($"admin/get-for-business/{businessId}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<AccountInfo>>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("The request failed to get accounts: " + e.Message);
                throw new InternalHttpRequestException(e);
            }

            throw new NotImplementedException("This should do something to offer a reload of the data.");
        }
    }
}
