using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;
using Services.Common.Exceptions;
using Services.Common.Extensions;
using Uri = System.Uri;

namespace Services.Businesses.Domain
{
    public class Business : IDomain
    {
        public Guid Id { get; }

        public string Name { get; }

        public string TradingName { get; }

        public Uri WebAddress { get; }

        public HeadOffice HeadOffice { get; }

        public HeadContact HeadContact { get; }

        public Business(string name, string tradingName, string webAddress, HeadOffice headOffice, HeadContact headContact)
        {
            if (name.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The name cannot be empty");
            if (tradingName.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The tradingName cannot be empty");
            WebAddress = new Uri(webAddress);
            Id = new Guid();
            Name = name;
            TradingName = tradingName;
            HeadOffice = headOffice;
            HeadContact = headContact;
        }
    }
}
