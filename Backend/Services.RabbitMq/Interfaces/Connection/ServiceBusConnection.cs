using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using Services.RabbitMq.Interfaces.Settings;

namespace Services.RabbitMq.Interfaces.Connection
{
    public class ServiceBusConnection : IServiceBusConnection
    {
        private IConnection _connection;
        public IServiceSettings ServiceSetting { get; set; }

        public void RegisterConnection(IConnection connection)
        {
            _connection?.Close();
            _connection?.Dispose();
            Channel?.Dispose();
            _connection = connection;
            Channel = _connection.CreateModel();
        }

        public IModel Channel
        {
            get;
            private set;
        }

        public void Close()
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}
