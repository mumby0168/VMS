using App.Businesses.Models;
using App.Businesses.Services;
using App.Shared.Events;
using App.Shared.Operations;
using App.Shared.Operations.Models;
using App.Shared.Services;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace App.Businesses.ViewModels
{
    public class UpdateContactDetailsViewModel
    {
        private readonly BusinessService _businessService;
        private readonly IOperationsManager _operationsService;
        private readonly IToastService _toastService;
        private readonly IPubSubService _pubSubService;
        private readonly IModalService _modalService;

        public UpdateContactDetailsViewModel(BusinessService businessService, IOperationsManager operationsService, IToastService toastService, IPubSubService pubSubService, IModalService modalService)
        {
            _businessService = businessService;
            _operationsService = operationsService;
            _toastService = toastService;
            _pubSubService = pubSubService;
            _modalService = modalService;
        }

        public Guid BusinessId { get; set; }

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

        public async Task SubmitAsync()
        {
            var res = await _businessService.UpdateContactAsync(new HeadContact { FirstName = FirstName, SecondName = SecondName, Email = Email, ContactNumber = ContactNumber }, BusinessId);
           
            if(res)
            {
                _modalService.Close(ModalResult.Cancel());                
                await _pubSubService.Publish<UpdateBusinessProfile>();                
            }           
        }
    }
}
