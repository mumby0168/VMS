using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http;
using Account.Interfaces.Jwt;
using Newtonsoft.Json;
using Account.Interfaces.Models;
using App.Shared.Models;
using Microsoft.Extensions.Logging;
using App.Shared.Http;

namespace App.Account.Services
{
    public class AccountService
    {
        public HttpClient Client { get; }
        private readonly ITokenStorageService _tokenStorage;
        private readonly ILogger<AccountService> _logger;
        private bool _isAuthHeaderSet = false;

        public AccountService(HttpClient client, ITokenStorageService tokenStorage, ILogger<AccountService> logger, Endpoints endpoints)
        {
            _tokenStorage = tokenStorage;
            _logger = logger;
            client.BaseAddress = new System.Uri(endpoints.Identity);
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
            HttpResponseMessage response;
            try
            {
                response = await Client.PostAsync("admin/sign-in-system", message);    
            }
            catch (HttpRequestException)
            {
                return false;
            }  

            if(response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<TokenResponse>(json);
                _logger.LogInformation("Token stored: " + res.Jwt);
                _tokenStorage.SaveToken(res.Jwt);
                return true;
            }

            return false;
        }

        public async Task<ServiceError> CreateAdmin(string email)
        {
            CheckHeaders();
            var message = new StringContent(JsonConvert.SerializeObject(new {Email = email}), Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                response = await Client.PostAsync("admin/create", message);    
            }
            catch (HttpRequestException)
            {
                return ServiceError.Standard;
            }  

            if(response.IsSuccessStatusCode)
            {
                return null;
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest) 
                return await ServiceError.Deserialize(response);
            return ServiceError.Standard;

        }

        public async Task<ServiceError> CompleteAdmin(string email, Guid code, string password, string passwordMatch)
        {
            var message = new StringContent(JsonConvert.SerializeObject(new {Email = email, Code = code, Password = password, PasswordMatch = passwordMatch}), Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                response = await Client.PostAsync("admin/complete", message);    
            }
            catch (HttpRequestException)
            {
                return ServiceError.Standard;
            }            
            if(response.IsSuccessStatusCode)
            {
                return null;
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest) 
                return await ServiceError.Deserialize(response);
            
            return ServiceError.Standard;
        }
    }
}