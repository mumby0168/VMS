using App.Businesses.Models;
using App.Businesses.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace App.Businesses.ViewModels
{
    public class CreateBusinessViewModel
    {
        private readonly ILogger<CreateBusinessViewModel> _logger;
        private readonly BusinessService _businessService;

        public CreateBusinessViewModel(ILogger<CreateBusinessViewModel> logger, BusinessService businessService)
        {
            _logger = logger;
            _businessService = businessService;
            Business = new Business();
        }

        public Business Business { get; set; }

        public async Task Submit()
        {
            var id = await _businessService.CreateBusiness(Business);
            if (id == null)
            {
                throw new NotImplementedException();
            }


        }
        
    }
}