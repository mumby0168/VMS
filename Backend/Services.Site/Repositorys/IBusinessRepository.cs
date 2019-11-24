using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Sites.Domain;

namespace Services.Sites.Repositorys
{
    public interface IBusinessRepository
    {
        Task AddAsync(IBusiness business);

        Task<bool> IsBusinessValidAsync(Guid id);
    }
}
    