using System;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Visitors.Events
{
    public class VisitorSignedIn : IEvent
    {
        public string VisitorName { get; set; }
        
        public string VisitingName { get; set; }
        
        public string Site { get; set; }
        
        public DateTime SignedInAt { get; set; }
        
        public string VisitingEmail { get; set; }
    }
}