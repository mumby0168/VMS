using System;

namespace Api.Gateway.Dtos.Visitors
{
    public class VisitorInformationDto
    {
        public Guid SpecificationId { get; set; }
        
        public string Label { get; set; }
        
        public string Value { get; set; }
    }
}