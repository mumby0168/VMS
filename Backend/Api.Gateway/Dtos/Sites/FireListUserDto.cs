using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.Dtos.Sites
{
    public class FireListUserDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Time { get; set; }

        public string Date { get; set; }
    }
}
