using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http;
using Account.Interfaces.Jwt;
using App.Account.Interfaces.Names;
using Newtonsoft.Json;
using Account.Interfaces.Models;
using App.Shared.Models;
using Microsoft.Extensions.Logging;

namespace App.Account.Services
{
    public class AccountService
    {
        public HttpClient Client { get; }
        private readonly ITokenStorageService _tokenStorage;
        private readonly ILogger<AccountService> _logger;
        private bool _isAuthHeaderSet = false;

        public AccountService(HttpClient client, ITokenStorageService tokenStorage, ILogger<AccountService> logger)
        {
            _tokenStorage = tokenStorage;
            _logger = logger;
            client.BaseAddress = new System.Uri(IdentityContext.IdentityBaseUrl);
            if(_tokenStorage.Token != null)
            {
                _isAuthHeaderSet = true;
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_tokenStorage.Token.RawToken}");
            }
            
            Client = client;
        }

        private void CheckHeaders()
        {
            if(_isAuthHeaderSet == false)
            {
                if(_tokenStorage.Token is null) 
                    _logger.LogWarning("Token still null on check before request.");
                else Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_tokenStorage.Token.RawToken}");
            }
        }

        public async Task<bool> SignIn(string username, string password)
        {
            var message = new StringContent(JsonConvert.SerializeObject(new {Email = username, Password = password}), Encoding.UTF8, "application/json");            
            var response = await Client.PostAsync("admin/sign-in-system", message);

            if(response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<TokenResponse>(json);
                _tokenStorage.SaveToken(res.Jwt);
                return true;
            }

            return false;
        }

        public async Task<ServiceError> CreateAdmin(string email)
        {
            CheckHeaders();
            var message = new StringContent(JsonConvert.SerializeObject(new {Email = email}), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("admin/create", message);

            if(response.IsSuccessStatusCode)
            {
                return null;
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var json = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ServiceError>(json);
                return res;
            }
            else 
            {
                return new ServiceError(string.Empty, "The service may be down. Please try again later.");
            }

        }
    }
}