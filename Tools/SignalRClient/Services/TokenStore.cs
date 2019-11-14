using SignalRClient.Interfaces;

namespace SignalRClient.Services
{
    public class TokenStore : ITokenStore
    {
        public string Token { get; set; }
    }
}