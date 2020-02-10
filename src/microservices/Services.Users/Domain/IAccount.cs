using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Users.Domain
{
    public interface IAccount : IDomain
    {
        string Email { get; }

        IAccount Create(Guid id, string email);
    }
}
