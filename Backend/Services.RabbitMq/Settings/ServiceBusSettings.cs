using System;
using System.Collections.Generic;
using System.Text;
using Services.RabbitMq.Interfaces.Settings;

namespace Services.RabbitMq.Settings
{
    public class ServiceBusSettings : IServiceBusSettings
    {
        public string HostName { get; set; }
    }
}
