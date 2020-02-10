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
    public class UpdateBusinessDetailsViewModel
    {
        private readonly BusinessService _businessService;
        private readonly IModalService _modalService;
        private readonly IPubSubService _pubSubService;

        public UpdateBusinessDetailsViewModel(BusinessService businessService, IModalService modalService, IPubSubService pubSubService)
        {
            _businessService = businessService;
            _modalService = modalService;
            _pubSubService = pubSubService;
        }

        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Business Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Trading Name")]
        public string TradingName { get; set; }
            
        [Url]
        [Display(Name = "Website")]
        public string WebAddress { get; set; }

        public async Task SubmitAsync()
        {
            var result = await _businessService.UpdateDetails(Id, Name, TradingName, WebAddress);
            if (result)
            {
                await _pubSubService.Publish<UpdateBusinessProfile>();
                _modalService.Close(ModalResult.Cancel());
            }

        }        
    }
}
