using System;

namespace Api.Gateway.Dtos.Visitors
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
