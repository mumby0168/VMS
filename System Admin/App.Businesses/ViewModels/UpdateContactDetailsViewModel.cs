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

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }

        public async Task SubmitAsync()
        {
            var id = await _businessService.UpdateContactAsync(new HeadContact { FirstName = FirstName, SecondName = SecondName, Email = Email, ContactNumber = ContactNumber }, BusinessId);
            var status = await _operationsService.GetOperationStatusAsync(id);
            if(status.Status == Shared.Operations.Models.OperationStatus.Complete)
            {
                _modalService.Close(ModalResult.Cancel());
                await _pubSubService.Publish<UpdateBusinessProfile>();
                _toastService.ShowSuccess("Contact details updated succesfully");                
            }
            else if(status is OperationMessageFailed failed)
            {
                _toastService.ShowError(failed.Reason);
            }

        }
    }
}
