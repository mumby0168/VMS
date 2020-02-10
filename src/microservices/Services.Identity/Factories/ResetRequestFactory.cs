using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Identity.Domain;

namespace Services.Identity.Factories
{
    public class ResetRequestFactory : IResetRequestFactory
    {
        public IResetRequest Create(string email)
        {
            return new ResetRequest().Create(email);
        }
    }
}
