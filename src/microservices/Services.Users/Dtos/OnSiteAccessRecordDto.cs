﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Users.Dtos
{
    public class OnSiteAccessRecordDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FullName { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
