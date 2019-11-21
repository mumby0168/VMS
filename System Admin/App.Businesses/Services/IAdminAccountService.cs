using App.Businesses.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Businesses.Services
{
    public interface IAdminAccountService
    {
        Task<IEnumerable<AccountInfo>> GetAccountsForBusiness(Guid businessId);

        Task<bool> DeleteAccount(Guid accountId, Guid businessId);
    }
}
