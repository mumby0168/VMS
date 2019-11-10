using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Services.Common.Extensions
{
    public static class Extensions
    {
        public static bool IsEmpty(this string @string) => string.IsNullOrWhiteSpace(@string);
    }
}
