using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Logs.Domain
{

    public enum LogType : byte
    {
        Info = 0,
        Warning = 1,
        Error = 2,
        Trace = 3,
    }


    public interface ILog : IDomain
    {
        ILog Create(string serviceName, string category, LogType logType, string messagae, Guid operationId,
            Guid userId);

        DateTime Created { get; }
        string ServiceName { get; }

        string Category { get; }

        LogType Type { get; }

        string Message { get; }

        Guid OperationId { get; }

        Guid UserId { get; }
    }
}
