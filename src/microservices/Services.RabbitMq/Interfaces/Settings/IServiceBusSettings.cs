using System;
using System.Collections.Generic;
using System.Text;

namespace Services.RabbitMq.Interfaces.Settings
{
    public interface IServiceBusSettings
    {
        string HostName { get; set; }
    }
}
