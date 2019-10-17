﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Identity.Domain
{
    public class PendingIdentity : IDomain
    {
        public Guid Id { get; private set; }

        public string Email { get; private set; }


        public PendingIdentity(Guid id, string email)
        {
            Id = id;
            Email = email;
        }

        public PendingIdentity()
        {
            
        }
    }
}
