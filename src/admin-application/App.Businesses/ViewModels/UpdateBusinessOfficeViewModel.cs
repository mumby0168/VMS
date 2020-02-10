using App.Businesses.Models;
using App.Businesses.Services;
using App.Shared.Events;
using App.Shared.Services;
using Blazored.Modal.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace App.Businesses.ViewModels
{
    public class UpdateBusinessOfficeViewModel
    {
        private readonly BusinessService _businessService;
        private readonly IModalService _modalService;
        private readonly IPubSubService _pubSubService;

        public UpdateBusinessOfficeViewModel(BusinessService businessService, IModalService modalService, IPubSubService pubSubService)
        {
            _businessService = businessService;
            _modalService = modalService;
            _pubSubService = pubSubService;
        }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Postcode")]
        [Required]
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        public Guid BusinessId { get; set; }

        public async Task SubmitAsync()
        {
            var result = await _businessService.UpdateOfficeAsync(new HeadOffice { PostCode = PostCode, AddressLine1 = AddressLine1, AddressLine2 = AddressLine2 }, BusinessId);
            if(result)
            {
                await _pubSubService.Publish<UpdateBusinessProfile>();
                _modalService.Close(ModalResult.Cancel());
            }

        }
    }
}
