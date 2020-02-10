using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using App.Account.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace App.Account.ViewModels {
    public class CompleteAdminViewModel {
        private readonly ILogger<CompleteAdminViewModel> _logger;
        private readonly AccountService _accountService;
        private readonly NavigationManager _navigationService;
        private readonly IToastService _toastService;
        public CompleteAdminViewModel (ILogger<CompleteAdminViewModel> logger, AccountService accountService, IToastService toastService, NavigationManager navigationService) {
            _toastService = toastService;
            _navigationService = navigationService;
            _accountService = accountService;
            _logger = logger;
        }

        public string Error { get; private set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordMatch { get; set; }

        public async Task Submit () {
            Error = string.Empty;
            if (!Guid.TryParse (Code, out var code)) {
                Error = "The code entered is invalid.";
                return;
            }
            if (Password != PasswordMatch) {
                Error = "The passwords entered do not match";
                return;
            }

            var result = await _accountService.CompleteAdmin (Email, code, Password, PasswordMatch);
            if (result is null) 
            {
                _toastService.ShowSuccess("Your account has been created.");
                _navigationService.NavigateTo("login");
                return;
            }

            Error = result.Reason;
        }

    }
}