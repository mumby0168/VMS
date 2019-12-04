using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Business.Dtos;
using Services.Business.Messages.Queries;
using Services.Business.Repositorys;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.Common.Queries;

namespace Services.Business.Handlers.Query
{
    public class GetBusinessQueryHandler : IQueryHandler<GetBusiness, BusinessDto>
    {
        private readonly IVmsLogger<GetBusinessQueryHandler> _logger;
        private readonly IBusinessRepository _repository;

        public GetBusinessQueryHandler(IVmsLogger<GetBusinessQueryHandler> logger,IBusinessRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<BusinessDto> HandleAsync(GetBusiness query)
        {
            var domain = await _repository.GetBusinessAsync(query.Id);

            if (domain is null)
            {
                _logger.LogInformation("No business found for the id: " + query.Id);
                return null;
            }

            return new BusinessDto()
            {
                Id = domain.Id,
                Name = domain.Name,
                TradingName = domain.TradingName,
                WebAddress = domain.WebAddress?.ToString(),
                Contact = new HeadContactDto()
                {
                    ContactNumber = domain.GetContact()?.ContactNumber, Email = domain.GetContact()?.Email,
                    FirstName = domain.GetContact()?.FirstName, SecondName = domain.GetContact()?.SecondName
                },
                Office = new HeadOfficeDto()
                {
                    AddressLine1 = domain.GetOffice()?.AddressLine1, AddressLine2 = domain.GetOffice()?.AddressLine2,
                    PostCode = domain.GetOffice()?.PostCode
                }
            };
        }
    }
}
