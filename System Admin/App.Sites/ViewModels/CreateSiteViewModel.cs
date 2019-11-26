using App.Sites.Models;
using App.Sites.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace App.Sites.ViewModels
{
    public class CreateSiteViewModel
    {
        private readonly SiteService _siteService;
        private readonly NavigationManager _navigationManager;

        public CreateSiteViewModel(SiteService siteService, NavigationManager navigationManager)
        {
            _siteService = siteService;
            _navigationManager = navigationManager;
        }


        public Guid BusinessId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]        
        public string PostCode { get; set; }

        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Phone]
        public string ContactNumber { get; set; }

        [EmailAddress]      
        public string Email { get; set; }

        public async Task SubmitAsync()
        {
            var success = await _siteService.CreateSiteAsync(new Site
            {
                BusinessId = BusinessId,
                Name = Name,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                PostCode = PostCode,
                FirstName = FirstName,
                SecondName = SecondName,
                Email = Email,
                ContactNumber = ContactNumber
            });

            if(success)
            {
                _navigationManager.NavigateTo($"/business-profile/{BusinessId.ToString()}");
            }
        }
    }
}
