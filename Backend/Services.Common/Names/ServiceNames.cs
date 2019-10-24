using System;
using System.Collections.Generic;
using System.Text;
using SharpCompress.Archives.SevenZip;

namespace Services.Common.Names
{
    public static class ServiceNames
    {
        private const string Prefix = "Services.";
        public static string Identity => Prefix + nameof(Identity);

        public static string Mail => Prefix + nameof(Mail);
    }
}
