using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity.Repositorys
{
    public interface IBusinessRepository
    {
        Task<bool> ContainsBusinessAsync(Guid business);
        Task<bool> IsCodeValidAsync(int businessCode);
    }
}
