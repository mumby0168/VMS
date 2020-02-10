using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Services.Common.Jwt
{
    public static class JwtExtensions
    {
        public static void AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration.GetSection("jwt:secret").Value;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };  
            });

            
            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient<IJwtManager, JwtManager>();
        }
    }
}
