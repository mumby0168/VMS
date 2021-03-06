﻿using System;

namespace Services.Users.Dtos
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }
        public Guid BasedSiteId { get; set; }
        
        public string Code { get; set; }
    }
}
