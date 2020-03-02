using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Api.Gateway.Messages.Visitors
{

    public class VisitorDataEntry
    {
        public Guid FieldId { get; }

        public string Value { get; }

        [JsonConstructor]
        public VisitorDataEntry(Guid fieldId, string value)
        {
            FieldId = fieldId;
            Value = value;
        }
    }

    
    [MicroService(Services.Common.Names.Services.Visitors)]
    public class CreateVisitor : ICommand
    {
        public Guid SiteId { get; }

        public Guid VisitingId { get; }

        public IEnumerable<VisitorDataEntry> Data { get; }

        [JsonConstructor]
        public CreateVisitor(Guid siteId, Guid visitingId, VisitorDataEntry[] data)
        {
            SiteId = siteId;
            VisitingId = visitingId;
            Data = data;
        }
    }
}