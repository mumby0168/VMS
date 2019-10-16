using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Exceptions
{
    public class VmsException : Exception
    {
        public string Code { get; }

        public VmsException(string code, string message) : base(message)
        {
            Code = code;
        }
    }
}
