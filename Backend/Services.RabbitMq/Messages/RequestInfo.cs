using System;
using System.Collections.Generic;
using System.Text;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Messages
{
    public class RequestInfo : IRequestInfo
    {
        public static IRequestInfo Empty => new RequestInfo();
    }
}
