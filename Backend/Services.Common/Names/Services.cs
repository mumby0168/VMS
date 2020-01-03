using System;
using System.Collections.Generic;
using System.Text;
using SharpCompress.Archives.SevenZip;

namespace Services.Common.Names
{
    public static class Services
    {
        private const string Prefix = "Services.";
        public const string Identity = Prefix + nameof(Identity);

        public const string Mail = Prefix + nameof(Mail);
        public const string Operations = Prefix + nameof(Operations);
        public const string Gateway = Prefix + nameof(Gateway);

        public const string Test = Prefix + nameof(Test);

        public const string Businesses = Prefix + nameof(Businesses);

        public const string Sites = Prefix + nameof(Sites);
        public static string Push => Prefix + nameof(Push);
        public static string Logs => Prefix + nameof(Logs);

        public const string Users = Prefix + nameof(Users);

        public const string Visitors = Prefix + nameof(Visitors);
    }
}
