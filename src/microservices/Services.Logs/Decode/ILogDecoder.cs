using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Logs.Domain;

namespace Services.Logs.Decode
{
    public interface ILogDecoder
    {
        ILog DecodeLog(byte[] data);
    }
}
