using Account.Interfaces.Jwt;
using Account.Jwt;
using Account.ViewModels;
using App.Account.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Account
{
    public static class Extensions
    {
        public  static IServiceCollection AddAccount(this IServiceCollection servies)
        {
            servies.AddSingleton<ITokenStorageService, TokenStorageService>();
            servies.AddScoped<LoginViewModel>();    
            servies.AddHttpClient<AccountService>();
            return servies;
        }   
    }
}