using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Identity.Domain
{
    public interface IResetRequest : IDomain
    {
        IResetRequest Create(string email);
        string Email { get;}

        DateTime RequestedAt { get; }
    }
}
