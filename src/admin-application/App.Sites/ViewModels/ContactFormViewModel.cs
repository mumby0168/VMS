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
    public class ContactFormViewModel
    {
        private readonly SiteService _siteService;
        private readonly IPubSubService _pubSubService;

        public ContactFormViewModel(SiteService siteService, IPubSubService pubSubService)
        {
            _siteService = siteService;
            this._pubSubService = pubSubService;
        }
        public void Setup(Guid siteId, string firstName, string secondName, string email, string contactNumber)
        {
            Id = siteId;
            FirstName = firstName;
            SecondName = secondName;
            ContactNumber = contactNumber;
            Email = email;
        }

        public bool IsEdit { get; set; }

        public Guid Id { get; set; }

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
            var res = await _siteService.UpdateSiteContactAsync(Id, FirstName, SecondName, Email, ContactNumber);
            if (res)
            {
                IsEdit = false;
                await _pubSubService.Publish<UpdateSiteProfile>();
            }
                

        }
    }
}
