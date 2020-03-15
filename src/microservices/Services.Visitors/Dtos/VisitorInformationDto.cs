using System;

namespace Services.Visitors.Dtos
{
    public class VisitorInformationDto
    {
        public Guid SpecificationId { get; set; }
        
        public string Label { get; set; }
        
        public string Value { get; set; }
    }
}