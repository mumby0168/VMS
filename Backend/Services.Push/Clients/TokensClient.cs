using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Convey.HTTP;

namespace Services.Push.Clients
{
    public class TokensClient : ITokensClient
    {
        private readonly IHttpClient _client;
        private readonly string _baseAddress;

        public TokensClient(IHttpClient client, HttpClientOptions options)
        {
            _client = client;
            _baseAddress = options.Services["identity:tokens"];
        }

        public async Task<bool> IsTokenValid(string token)
        {
            var response = await _client.PostAsync($"{_baseAddress}/validate");
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
