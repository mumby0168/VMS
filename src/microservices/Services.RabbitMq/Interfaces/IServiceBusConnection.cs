using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using Services.RabbitMq.Interfaces.Settings;

namespace Services.RabbitMq.Interfaces
{
    public interface IServiceBusConnection
    {
        IServiceSettings ServiceSetting { get; set; }
        void RegisterConnection(IConnection connection);

        IModel Channel { get; }

        void Close();
    }
}
