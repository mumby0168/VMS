using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Logging
{
    public interface IVmsLogger<T> where T : class
    {
        void LogInformation(string message, string category = null);

        void LogWarning(string message, string category = null);

        void LogTrace(string message, string category = null);
        void LogError(string message, string category = null);
    }
}
