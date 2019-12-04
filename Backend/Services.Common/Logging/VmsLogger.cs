using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Services.Common.Logging
{
    public class VmsLogger<T> : IVmsLogger<T> where T : class
    {
        private readonly ILogger<T> _standardLogger;
        private readonly IUdpLoggingClient _logger;

        public VmsLogger(ILogger<T> standardLogger, IUdpLoggingClient logger)
        {
            _standardLogger = standardLogger;
            _logger = logger;
        }


        public void LogInformation(string message, string category = null)
        {
            _standardLogger.LogInformation(message);
            Log(message, LogType.Info, category);
        }

        public void LogWarning(string message, string category = null)
        {
            _standardLogger.LogWarning(message);
            Log(message, LogType.Warning, category);
        }

        public void LogTrace(string message, string category = null)
        {
            _standardLogger.LogTrace(message);
            Log(message, LogType.Trace, category);
        }

        public void LogError(string message, string category = null)
        {
            _standardLogger.LogError(message);
            Log(message, LogType.Error, category);
        }

        private void Log(string message, LogType type, string category = null)
        {
            Task.Run(() =>
            {
                string cat = category ?? "None";
                _logger.LogAsync(cat, type, message, Guid.Empty, Guid.Empty);
            });
        }
    }
}
