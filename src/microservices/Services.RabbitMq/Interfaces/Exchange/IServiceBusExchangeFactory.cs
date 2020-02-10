using System;
using System.Collections.Generic;
using System.Text;

namespace Services.RabbitMq.Interfaces.Exchange
{
    public interface IServiceBusExchangeFactory
    {
        IServiceBusExchange CreateServiceBusExchange();
    }
}
