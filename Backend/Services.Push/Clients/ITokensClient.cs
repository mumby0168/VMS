using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Push.Clients
{
    public interface ITokensClient
    {
        Task<bool> IsTokenValid(string token);
    }
}
