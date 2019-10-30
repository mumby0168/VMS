using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.RabbitMq.Messages
{
    public class RequestInfo : IRequestInfo
    {
        public static IRequestInfo Empty => new RequestInfo();

        public static IRequestInfo Create(Guid userId) => new RequestInfo(userId);
        public Guid OperationId { get; }
        public Guid UserId { get; }
        public DateTime Created { get; }
        public DateTime Completed { get; private set; }
        public RequestState State { get; }
        public string Code { get; private set; }
        public string Reason { get; private set; }

        [JsonConstructor]
        public RequestInfo(Guid operationId, Guid userId, DateTime created, DateTime completed, RequestState state, string code, string reason)
        {
            OperationId = operationId;
            UserId = userId;
            Created = created;
            Completed = completed;
            State = state;
            Code = code;
            Reason = reason;
        }

        public RequestInfo()
        {
            
        }

        public RequestInfo(Guid userId)
        {
            UserId = userId;
            OperationId = Guid.NewGuid();
            Created = DateTime.Now;
            State = RequestState.Pending;
            
        }
    }
}
