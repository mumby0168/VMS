namespace Account.Interfaces.Models
{
    public class TokenResponse
    {
        public string Jwt { get; set; }

        public string RefreshToken { get; set; }
    }
}