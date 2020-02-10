using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Xml.Schema;
using App.Account.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;

namespace App.Account.ViewModels
{
    public class CreateAdminViewModel
    {
        private readonly AccountService _accountService;
        private readonly IToastService _toastService;
        private readonly NavigationManager _navigationManager;
        public CreateAdminViewModel(AccountService accountService, IToastService toastService, NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _toastService = toastService;
            _accountService = accountService;
            Error = string.Empty;
        }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string Error { get; set; }

        public async Task Submit()
        {
            var result = await _accountService.CreateAdmin(Email);
            if (result is null)
            {
                _navigationManager.NavigateTo("counter");
                _toastService.ShowSuccess($"Email sent to {Email} to setup their account.");
                Email = string.Empty;
                Error = string.Empty;
                return;
            }
            Error = result.Reason;
        }
    }
}