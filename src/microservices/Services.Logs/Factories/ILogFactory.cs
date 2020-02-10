using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Logs.Domain;

namespace Services.Logs.Factories
{
    public interface ILogFactory
    {
        ILog CreateLog(string serviceName, string category, LogType logType, string messagae, Guid operationId,
            Guid userId);
    }
}
