using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Push.Services
{
    public interface IPushService
    {
        Task OperationComplete(Guid opId, Guid userId);

        Task OperationFailed(Guid opId, Guid userId, string code, string reason);
    }
}
