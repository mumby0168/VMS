using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Visitors.Commands
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


    public class CreateVisitor : ICommand
    {
        public Guid SiteId { get; }

        public Guid VisitingId { get; }

        public IEnumerable<VisitorDataEntry> Data { get; }

        public CreateVisitor(Guid siteId, Guid visitingId, IEnumerable<VisitorDataEntry> data)
        {
            SiteId = siteId;
            VisitingId = visitingId;
            Data = data;
        }
    }
}