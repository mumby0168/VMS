using App.Shared.Events;
using App.Shared.Services;
using App.Sites.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace App.Sites.ViewModels
{
    public class AddressFormViewModel
    {
        private readonly SiteService _siteService;
        private readonly IPubSubService _pubSubService;

        public AddressFormViewModel(SiteService siteService, IPubSubService pubSubService)
        {
            _siteService = siteService;
            this._pubSubService = pubSubService;
        }
        public void Setup(Guid siteId, string name, string addressLine1, string addressLine2, string postCode)
        {
            Id = siteId;
            Name = name;
            PostCode = postCode;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
        }

        public bool IsEdit { get; set; }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PostCode { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public async Task SubmitAsync()
        {
            var res = await _siteService.UpdateSiteDetailsAsync(Id, Name, AddressLine1, AddressLine2, PostCode);
            if (res)
            {
                IsEdit = false;
                await _pubSubService.Publish<UpdateSiteProfile>();
            }
                
        }
    }
}
