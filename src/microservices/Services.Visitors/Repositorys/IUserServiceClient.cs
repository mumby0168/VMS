using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Visitors.Dtos;

namespace Services.Visitors.Repositorys
{
    public interface IUserServiceClient
    {
        Task<bool> ContainsUserAsync(Guid userId);
        Task<UserDto> GetUserAsync(Guid messageVisitingId);
    }
}
