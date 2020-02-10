using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Exceptions
{
    public class VmsServiceDownException : Exception
    {
        public VmsServiceDownException(string service) : base("The service is temporarily unavailable")
        {
            Service = service;
        }

        public string Service { get; }
    }
}
