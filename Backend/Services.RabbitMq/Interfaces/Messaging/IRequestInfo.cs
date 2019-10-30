using System;
using System.Collections.Generic;
using System.Text;

namespace Services.RabbitMq.Interfaces.Messaging
{
    public enum RequestState : byte
    {
        Pending,
        Failed,
        Complete
    }
    public interface IRequestInfo
    {
        Guid OperationId { get; }
        Guid UserId { get; }
        DateTime Created { get; }
        DateTime Completed { get; }
        RequestState State { get; }
        string Code { get; }
        string Reason { get; }

    }
}
