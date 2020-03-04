using System.Collections.Generic;

namespace Services.Visitors.Domain
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
        