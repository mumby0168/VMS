using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Logging
{
    public interface IUdpLoggingClient
    {
        void CreateConnection();

        bool IsConnected { get; }

        Task LogAsync(string category, LogType type, string message, Guid operationId, Guid userId);

        void Disconnect();
    }
}
