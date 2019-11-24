using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ISite = Services.Sites.Domain.ISite;

namespace Services.Sites.Repositorys
{
    public interface ISiteRepository
    {
        Task AddAsync(ISite site);
    }
}
