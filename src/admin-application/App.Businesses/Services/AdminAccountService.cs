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
        private readonly IHttpExecutor _executor;
        private readonly Endpoints _endpoints;

        public AdminAccountService(ILogger<AdminAccountService> logger, IHttpClient client, IToastService toastService, IHttpExecutor executor, Endpoints endpoints)
        {
            _logger = logger;
            _client = client;
            _toastService = toastService;
            _executor = executor;
            this._endpoints = endpoints;
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

        public Task<IEnumerable<AccountInfo>> GetAccountsForBusiness(Guid businessId) => _executor.GetAsync<IEnumerable<AccountInfo>>(_endpoints.Identity + $"admin/get-for-business/{businessId}");      
    }
}
