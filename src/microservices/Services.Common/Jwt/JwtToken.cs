using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;

namespace Services.Common.Jwt
{
    public class JwtToken : IJwtToken
    {

        private readonly DateTime DateFrom = new DateTime(1970, 1, 1);

        private JwtToken(string raw)
        {
            RawToken = raw;
            ProcessToken();
        }

        private void ProcessToken()
        {
            string payload = RawToken.Split('.')[1];
            var bytes = ParseBase64WithoutPadding(payload);
            var json = Encoding.ASCII.GetString(bytes);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            if (dictionary.TryGetValue("role", out object role))
            {
                Role = role.ToString();
            }
            if (dictionary.TryGetValue("exp", out object expiryInSeconds))
            {
                CalculateExpiry((Int64)expiryInSeconds);
            }
            if (dictionary.TryGetValue("nameid", out object id))
            {
                Id = Guid.Parse(id.ToString());
            }
            if(dictionary.TryGetValue(CustomClaims.BusinessIdClaim, out object businessId))
            {
                BusinessId = Guid.Parse((string) businessId);
            }

        }

        private void CalculateExpiry(Int64 secondsSince)
        {
            Expiry = DateFrom.AddSeconds(secondsSince);
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }

        public static IJwtToken Process(string raw) => new JwtToken(raw);

        public string RawToken { get; }

        public string Role { get; private set; }
        public Guid Id { get; private set; }

        public IEnumerable<Claim> Claims { get; private set; }

        public DateTime Expiry { get; private set; }
        public Guid BusinessId { get; private set; }
    }
}
