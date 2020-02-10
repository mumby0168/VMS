using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Logs.Server
{
    public interface IUdpServer
    {
        void Begin(int port);

        void Stop();
    }
}
