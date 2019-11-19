using App.Businesses.Models;
using App.Businesses.Services;
using App.Shared.Operations;
using App.Shared.Operations.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace App.Businesses.ViewModels
{
    public class CreateBusinessViewModel
    {
        private readonly ILogger<CreateBusinessViewModel> _logger;
        private readonly BusinessService _businessService;
        private readonly IToastService _toastService;
        private readonly IOperationsManager _operationManager;
        private readonly NavigationManager _navigationManager;

        public CreateBusinessViewModel(ILogger<CreateBusinessViewModel> logger, BusinessService businessService, IToastService toastService, IOperationsManager operationManager, NavigationManager navigationManager)
        {
            _logger = logger;
            _businessService = businessService;
            _toastService = toastService;
            _operationManager = operationManager;
            _navigationManager = navigationManager;            
        }

        [Required]
        [Display(Name = "Business Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Trading Name")]
        public string TradingName { get; set; }

        [Url]
        [Display(Name = "Website")]
        public string WebAddress { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Postcode")]
        [Required]        
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        private void Clear()
        {
            Name = string.Empty;
            TradingName = string.Empty;
            WebAddress = string.Empty;
            FirstName = string.Empty;
            SecondName = string.Empty;
            Email = string.Empty;
            ContactNumber = string.Empty;
            PostCode = string.Empty;
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
        }

        public async Task Submit()
        {
            var id = await _businessService.CreateBusiness(
            new Business
            {
                Name = Name,
                TradingName = TradingName,
                WebAddress = WebAddress,
                Contact = new HeadContact { FirstName = FirstName, SecondName = SecondName, Email = Email, ContactNumber = ContactNumber },
                Office = new HeadOffice { PostCode = PostCode, AddressLine1 = AddressLine1, AddressLine2 = AddressLine2 }
            });
            if (id == null)
            {
                throw new NotImplementedException();
            }

            var operation = await _operationManager.GetOperationStatusAsync(id);
            if (operation is null)
            {
                _toastService.ShowError("Failed to get reponse info");
                return;
            }
            if (operation.Status == OperationStatus.Complete)
            {
                _navigationManager.NavigateTo("/businesses");
                _toastService.ShowSuccess($"The business {Name} was succsefully created.");
                Clear();
            }                
            else if(operation.Status == OperationStatus.Failed)
            {
                var failed = operation as IOperationMessageFailed;
                _toastService.ShowError(failed.Reason);
            }
        }
        
    }
}