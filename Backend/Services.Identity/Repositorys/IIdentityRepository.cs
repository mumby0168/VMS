using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity.Repositorys
{
    public interface IIdentityRepository
    {
        bool IsEmailInUse(string email);
    }
}
