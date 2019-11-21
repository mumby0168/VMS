﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Identity.Domain
{
    public class Business : IDomain
    {
        public Guid Id { get; private set; }

        public Business(Guid id)
        {
            Id = id;
        }
    }
}
