using System.Text;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Newtonsoft.Json;

namespace System_Admin.Areas.Account.Models
{
    public class JwtToken : IJwtToken
    {
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
            if(dictionary.TryGetValue("role", out object role))
            {
                Role = role.ToString();
            }
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public static IJwtToken Process(string raw) => new JwtToken(raw);

        public string RawToken { get; }

        public string Role { get; private set; }

        public IEnumerable<Claim> Claims { get; private set; }
    }
}