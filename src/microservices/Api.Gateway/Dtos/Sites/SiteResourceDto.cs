﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Dtos.Sites
{
    public class SiteResourceDto
    {
        public Guid Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
    }
}
