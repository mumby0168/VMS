using App.Sites.Services;
using Blazored.Modal.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace App.Sites.ViewModels
{
    public class CreateSiteResourceViewModel
    {
        private readonly SiteService _siteService;
        private readonly IModalService _modalService;

        public CreateSiteResourceViewModel(SiteService siteService, IModalService modalService)
        {
            _siteService = siteService;
            _modalService = modalService;
        }

        public Guid SiteId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Identifier { get; set; }

        public async Task SubmitAsync()
        {
            var res = await _siteService.CreateSiteResource(SiteId, Name, Identifier);
            if(res)
            {
                _modalService.Close(ModalResult.Cancel());
                // something to reload list.
            }
        }
    }
}
