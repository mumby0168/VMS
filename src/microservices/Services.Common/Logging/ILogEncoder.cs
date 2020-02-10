using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Logging
{
    public interface ILogEncoder
    {
        byte[] EncodeLog(string serviceName, string category, LogType type, string message, Guid operationId, Guid userId);
    }
}
