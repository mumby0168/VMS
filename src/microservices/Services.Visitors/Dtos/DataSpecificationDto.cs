using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Visitors.Dtos
{
    public class DataSpecificationDto
    {
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string ValidationCode { get; set; }

        public string ValidationMessage { get; set; }

        public int Order { get; set; }
        
        public bool IsMandatory { get; set; }
    }
}
