using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Visitors.Names
{
    public static class Validation
    {
        public static IEnumerable<string> Options => new List<string>()
        {
            "Required",
            "Email",
            "Post Code"
        };
    }
}
        