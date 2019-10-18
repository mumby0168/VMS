using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System_Admin.Services;
using Microsoft.Extensions.Logging;

namespace System_Admin.Clients.Account
{
    public class AccountClient : IAccountClient
    {
        private const string Url = "http://localhost:5000/api/admin/";
        private readonly IHttpClientFactory _clientFactory;
        private readonly ITokenStorageService _tokenStorageService;
        private readonly ILogger<AccountClient> _logger;

        public AccountClient(IHttpClientFactory clientFactory, ITokenStorageService tokenStorageService, ILogger<AccountClient> logger)
        {
            this._logger = logger;
            _clientFactory = clientFactory;
            this._tokenStorageService = tokenStorageService;
        }
        public async Task<bool> SignIn(string email, string password)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, Url + "sign-in-system");        
            request.Content = new StringContent(JsonConvert.SerializeObject(new { Email = email, Password = password }), Encoding.UTF8, "application/json");
        
            var response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<TokenResponse>(json);
                _tokenStorageService.SaveToken(token.Jwt);
                return true;
            }
            else
            {
                _logger.LogInformation($"sign-in failed: {response.StatusCode}");
                return false;
            }
        }
    }
}