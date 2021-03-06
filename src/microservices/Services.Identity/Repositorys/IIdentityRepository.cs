﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity.Repositorys
{
    public interface IIdentityRepository
    {
        Task<bool> IsEmailInUse(string email, string role);

        Task<Domain.Identity> GetByEmailAndRole(string email, string role);
        Task AddAsync(Domain.Identity identity);
        Task<IEnumerable<Domain.Identity>> GetAdminsForBusinessAsync(Guid businessId);
        Task<Domain.Identity> GetAsync(Guid id, Guid businessId);
        Task RemoveAsync(Domain.Identity identity);
        Task<Domain.Identity> GetByEmail(string email);
        Task UpdateAsync(Domain.Identity identity);

        Task<IEnumerable<Domain.Identity>> GetStandardAccountsForBusinessAsync(Guid businessId);
        Task<Domain.Identity> GetStandardAccountsForBusinessAsync(Guid businessId, Guid accountId);
        Task<bool> IsCodeInUseAsync(int number, Guid businessid);
    }
}
