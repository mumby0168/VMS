﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Identity.Dtos;
using Services.Identity.Messages.Commands;
using Services.Identity.Models;

namespace Services.Identity.Services
{
    public interface IUserService
    {
        Task CompleteUser(Guid code, string email, string password, string passwordConfirm);
        Task<IAuthToken> SignIn(string email, string password);
        Task InitiatePasswordReset(string email);
        Task ResetPassword(Guid code, string email, string password, string passwordConfirm);
        Task CreateUser(string email, Guid businessId);

        IAsyncEnumerable<StandardUserAccountDto> GetStandardAccountsForBusiness(Guid businessId);

        Task RemoveAsync(Guid accountId, Guid businessId);
        Task<IEnumerable<PendingAccountDto>> GetPendingAccountsForBusinessAsync(Guid businessId);
        Task RemovePendingAsync(Guid pendingId, Guid businessId);   
    }
}
