using System;
using System.Collections.Generic;
using System.Text;

namespace Services.RabbitMq.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class)]
    public class MicroService : Attribute
    {
        public string ServiceName { get; }

        public MicroService(string serviceName)
        {
            ServiceName = serviceName;
        }
    }
}
