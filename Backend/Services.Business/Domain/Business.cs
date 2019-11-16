﻿using System;
using Services.Common.Domain;
using Services.Common.Exceptions;
using Services.Common.Extensions;
using Uri = System.Uri;

namespace Services.Business.Domain
{
    public class Business : IDomain
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string TradingName { get; private set; }

        public Uri WebAddress { get; private set; }

        public HeadOffice HeadOffice { get; private set; }

        public HeadContact HeadContact { get; private set; }

        public Business()
        {
            
        }

        public Business(string name, string tradingName, string webAddress, HeadOffice headOffice, HeadContact headContact)
        {
            if (name.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The name cannot be empty");
            if (tradingName.IsEmpty()) throw new VmsException(Codes.EmptyProperty, "The tradingName cannot be empty");
            try
            {
                WebAddress = new Uri(webAddress);
            }
            catch (Exception)
            {
                // ignored
            }

            Id = new Guid();
            Name = name;
            TradingName = tradingName;
            HeadOffice = headOffice;
            HeadContact = headContact;
        }
    }
}
