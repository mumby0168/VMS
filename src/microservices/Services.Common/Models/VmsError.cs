using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Models
{
    public class VmsError
    {
        public string Code { get; }

        public string Reason { get; }

        private VmsError() { }

        private VmsError(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }

        public static VmsError Create(string code, string reason) => new VmsError(code, reason);
    }
}
