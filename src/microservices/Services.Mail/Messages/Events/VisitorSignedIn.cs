using System;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Mail.Messages.Events
{
    [MicroService(Services.Common.Names.Services.Visitors)]
    public class VisitorSignedIn : IEvent
    {
        public string VisitorName { get; set; }
        
        public string VisitingName { get; set; }
        
        public string Site { get; set; }
        
        public DateTime SignedInAt { get; set; }
        
        public string VisitingEmail { get; set; }
    }
}