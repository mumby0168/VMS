using Account.Interfaces.Jwt;
using Account.Jwt;
using Account.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Account
{
    public static class Extensions
    {
        public  static IServiceCollection AddAccount(this IServiceCollection servies)
        {
            servies.AddSingleton<ITokenStorageService, TokenStorageService>();
            servies.AddScoped<LoginViewModel>();
            return servies;
        }   
    }
}