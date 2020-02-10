using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Services.Push.Hubs;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Push.Services
{
    public class PushService : IPushService
    {
        private readonly IHubContext<VmsHub> _hub;

        public PushService(IHubContext<VmsHub> hub)
        {
            _hub = hub;
        }
        public Task OperationComplete(Guid opId, Guid userId) =>
            _hub.Clients.Group($"users:{userId.ToString()}").SendAsync("operationComplete",
                new {OperationId = opId, Status = RequestState.Complete});

        public Task OperationFailed(Guid opId, Guid userId, string code, string reason) =>
            _hub.Clients.Group($"users:{userId.ToString()}").SendAsync("operationFailed",
                new { OperationId = opId, Status = RequestState.Failed, Code = code, Reason = reason });
    }
}
