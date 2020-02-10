using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Repositories
{
    public interface ISiteRepository
    {
        Task<bool> IsSiteIdValid(Guid id);

        Task<string> GetSiteNameAsync(Guid id);
        Task AddSiteAsync(ISite site);
    }
}
