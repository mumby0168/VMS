using System;
using System.Collections.Generic;
using System.Text;
using SharpCompress.Archives.SevenZip;

namespace Services.Common.Names
{
    public static class ServiceNames
    {
        private const string Prefix = "Services.";
        public const string Identity = Prefix + nameof(Identity);

        public const string Mail = Prefix + nameof(Mail);
        public static string Operations => Prefix + nameof(Operations);
        public const string Gateway = Prefix + nameof(Gateway);

        public static string Test => Prefix + nameof(Test);

        public static string Businesses => Prefix + nameof(Businesses);

        public static string Sites => Prefix + nameof(Sites);
    }
}
