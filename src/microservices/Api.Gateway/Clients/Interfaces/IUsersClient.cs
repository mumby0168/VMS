using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Gateway.Dtos.Users;

namespace Api.Gateway.Clients.Interfaces
{
    public interface IUsersClient
    {
        Task<IEnumerable<AccessRecordDto>> GetAccessRecordForUserAsync(Guid accountId);
        Task<IEnumerable<SiteAccessDetailsDto>> GetBusinessAccessRecordsAsync(Guid businessId);
        Task<UserInfoDto> GetUserInfo(Guid accountId);
        Task<IEnumerable<UserSnapshotDto>> GetUsersForBusiness(Guid businessId);
        Task<IEnumerable<AccessRecordDto>> GetAccessRecordForUserAsyncById(Guid userId);
    }
}
        