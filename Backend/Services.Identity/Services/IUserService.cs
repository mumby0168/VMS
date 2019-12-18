using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Identity.Messages.Commands;

namespace Services.Identity.Services
{
    public interface IUserService
    {
        Task CompleteUser(Guid code, string email, string password, string passwordConfirm);
    }
}
