using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Visitors.Repositorys
{
    public interface IUserServiceClient
    {
        Task<bool> ContainsUserAsync(Guid userId);
    }
}
