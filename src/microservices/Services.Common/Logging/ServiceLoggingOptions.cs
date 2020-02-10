using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Logging
{
    public class ServiceLoggingOptions
    {
        public string ServiceName { get; set; }

        public int Port { get; set; }

        public string Address { get; set; }
    }
}
