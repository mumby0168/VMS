using System.Runtime.InteropServices;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http;
using Account.Interfaces.Jwt;
using App.Account.Interfaces.Names;
using Newtonsoft.Json;
using Account.Interfaces.Models;

namespace App.Account.Services
{
    public class AccountService
    {
        public HttpClient Client { get; }
        private readonly ITokenStorageService _tokenStorage;

        public AccountService(HttpClient client, ITokenStorageService tokenStorage)
        {
            this._tokenStorage = tokenStorage;            
            client.BaseAddress = new System.Uri(IdentityContext.IdentityBaseUrl);
            if(_tokenStorage.Token != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_tokenStorage.Token.RawToken}");
            }
            
            Client = client;
        }

        public async Task<bool> SignIn(string username, string password)
        {
            var message = new StringContent(JsonConvert.SerializeObject(new {Email = username, Password = password}), Encoding.UTF8, "application/json");            
            var response = await Client.PostAsync("admin/sign-in-system", message);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<TokenResponse>(json);
                _tokenStorage.SaveToken(res.Jwt);
                return true;
            }

            return false;
        }
    }
}