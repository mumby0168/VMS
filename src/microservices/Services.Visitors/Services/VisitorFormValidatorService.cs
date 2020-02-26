using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Exceptions;
using Services.Common.Logging;
using Services.Visitors.Commands;
using Services.Visitors.Names;
using Services.Visitors.Repositorys;

namespace Services.Visitors.Services
{
    public class VisitorFormValidatorService : IVisitorFormValidatorService
    {
        private readonly IVmsLogger<VisitorFormValidatorService> _logger;
        private readonly IDataSpecificationRepository _specificationRepository;
        private readonly IDataSpecificationValidator _specificationValidator;

        public VisitorFormValidatorService(IVmsLogger<VisitorFormValidatorService> logger, IDataSpecificationRepository specificationRepository, IDataSpecificationValidator specificationValidator)
        {
            _logger = logger;
            _specificationRepository = specificationRepository;
            _specificationValidator = specificationValidator;
        }
        public async Task Validate(Guid businessId, IEnumerable<VisitorDataEntry> data)
        {
            var dataSpecs = await _specificationRepository.GetEntriesAsync(businessId);

            var dataSpecificationDocuments = dataSpecs.ToList();
            
            if (!dataSpecificationDocuments.Any())
            {
                _logger.LogError($"No data specs for business with id {businessId}");
                throw new VmsException(Codes.InvalidId, "There are no data fields for the business.");
            }
            
            foreach (var entry in data)
            {
                var spec = dataSpecificationDocuments.FirstOrDefault(s => s.Id == entry.FieldId);
                if (spec is null)
                {
                    _logger.LogError($"No data specification found with id: {entry.FieldId}");
                    throw new VmsException(Codes.InvalidId, "The data presented does not match our specification");
                }

                if (!_specificationValidator.IsDataValid(entry.Value, spec.ValidationCode))
                {
                    throw new VmsException(Codes.ValidationError, spec.ValidationMessage);
                }
            }
        } 
        
    }
}