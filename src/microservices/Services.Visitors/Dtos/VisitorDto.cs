using System;

namespace Services.Visitors.Dtos
{
    public class VisitorDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string InAt { get; set; }
    }
}