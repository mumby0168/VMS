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

        public AddressFormViewModel(SiteService siteService)
        {
            _siteService = siteService;
        }
        public void Setup(Guid siteId, string addressLine1, string addressLine2, string postCode)
        {
            Id = siteId;
            PostCode = postCode;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
        }

        public Guid Id { get; set; }

        [Required]
        public string PostCode { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public async Task SubmitAsync()
        {

        }
    }
}
