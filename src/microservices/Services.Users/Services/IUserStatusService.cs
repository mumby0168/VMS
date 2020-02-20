using System;
using System.Threading.Tasks;
using Services.Users.Domain;

namespace Services.Users.Services
{
    public interface IUserStatusService
    {
        Task Update(Guid userId, AccessAction action, Guid SiteId);
    }
}
