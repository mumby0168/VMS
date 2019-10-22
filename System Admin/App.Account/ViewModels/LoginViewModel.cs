using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using App.Account.Services;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Account.ViewModels {
    public class LoginViewModel {
        private readonly AccountService _accountService;
        private readonly NavigationManager _navigationManager;
        private readonly ILogger<LoginViewModel> _logger;
        public LoginViewModel (AccountService accountService, NavigationManager navigationManager, ILogger<LoginViewModel> logger) {
            this._logger = logger;
            this._navigationManager = navigationManager;
            this._accountService = accountService;
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
                _navigationManager.NavigateTo ("counter");
                return;
            }
        
            Error = "Invalid credentials please try again.";

            _logger.LogInformation($"Unsuccesful login for: {Email}.");

        }
    }
}