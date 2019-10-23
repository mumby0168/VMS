using System.Threading;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using App.Account.Services;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Authorization;

namespace Account.ViewModels {
    public class LoginViewModel {
        private readonly AccountService _accountService;
        private readonly NavigationManager _navigationManager;
        private readonly ILogger<LoginViewModel> _logger;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public LoginViewModel (AccountService accountService, NavigationManager navigationManager, ILogger<LoginViewModel> logger, AuthenticationStateProvider authenticationStateProvider) {
            _logger = logger;
            _authenticationStateProvider = authenticationStateProvider;
            _navigationManager = navigationManager;
            _accountService = accountService;
            Error = string.Empty;
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Error { get; private set; }

        public async Task Submit () {

            Error = string.Empty;

            var success = await _accountService.SignIn(Email, Password);
            if (success) {
                _logger.LogInformation($"Succesful login for: {Email}.");       
                     
                _navigationManager.NavigateTo("counter");            
                return;
            }
        
            Error = "Invalid credentials please try again.";

            _logger.LogInformation($"Unsuccesful login for: {Email}.");

        }
    }
}