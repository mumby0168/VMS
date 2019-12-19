using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Identity.Domain;

namespace Services.Identity.Factories
{
    public interface IResetRequestFactory
    {
        IResetRequest Create(string email);
    }
}
