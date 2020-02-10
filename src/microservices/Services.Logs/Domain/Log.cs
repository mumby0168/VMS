using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Logs.Domain
{
    public class Log : ILog
    {
        public Guid Id { get; private set; }
        public ILog Create(string serviceName, string category, LogType logType, string message, Guid operationId, Guid userId)
        {
            Id = new Guid();
            Created = DateTime.UtcNow;
            ServiceName = serviceName;
            Category = category;
            Type = logType;
            Message = message;
            OperationId = operationId;
            UserId = userId;
            return this;
        }

        public DateTime Created { get; private set; }
        public string ServiceName { get; private set; }
        public string Category { get; private set; }
        public LogType Type { get; private set; }
        public string Message { get; private set; }
        public Guid OperationId { get; private set; }
        public Guid UserId { get; private set; }
    }
}
