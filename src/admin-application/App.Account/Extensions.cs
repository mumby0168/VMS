using Account.Interfaces.Jwt;
using Account.Jwt;
using Account.ViewModels;
using App.Account.Services;
using App.Account.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Account
{
    public static class Extensions
    {
        public  static IServiceCollection AddAccount(this IServiceCollection servies)
        {
            servies.AddSingleton<ITokenStorageService, TokenStorageService>();
            servies.AddScoped<LoginViewModel>();    
            servies.AddScoped<CreateAdminViewModel>(); 
            servies.AddScoped<CompleteAdminViewModel>();    
            servies.AddHttpClient<AccountService>();
            return servies;
        }   
    }
}