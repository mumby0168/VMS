﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.RabbitMq.Interfaces.Messaging
{
    public interface IGenericEventHandler
    {
        Task HandleAsync(object message, IRequestInfo requestInfo);
    }
}
