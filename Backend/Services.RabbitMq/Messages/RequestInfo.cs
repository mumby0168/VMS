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
        public RequestState State { get; private set; }
        public void Complete()
        {
            State = RequestState.Complete;
            Completed = DateTime.Now;
        }

        public void Fail()
        {
            State = RequestState.Failed;
            Completed = DateTime.Now;
        }


        [JsonConstructor]
        public RequestInfo(Guid operationId, Guid userId, DateTime created, DateTime completed, RequestState state)
        {
            OperationId = operationId;
            UserId = userId;
            Created = created;
            Completed = completed;
            State = state;
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
