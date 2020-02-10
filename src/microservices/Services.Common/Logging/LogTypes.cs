using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Logging
{
    public enum LogType : byte
    {
        Info = 0,
        Warning = 1,
        Error = 2,
        Trace = 3,
    }
}
