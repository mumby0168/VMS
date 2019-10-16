using System;
using System.Collections.Generic;
using System.Text;
using Services.RabbitMq.Interfaces.Settings;

namespace Services.RabbitMq.Settings
{
    public class ServiceSettings : IServiceSettings
    {
        public string Name { get; set; }
    }
}
