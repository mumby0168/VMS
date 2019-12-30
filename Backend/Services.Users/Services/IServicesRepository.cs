using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Users.Services.Dtos;

namespace Services.Users.Services
{
    public interface IServicesRepository
    {
        Task<bool> IsSiteIdValid(Guid siteId);

        Task<bool> IsBusinessIdValid(Guid businessId);

        Task<string> GetSiteNameAsync(Guid siteId);
    }
}
