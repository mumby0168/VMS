using System;
using Services.Common.Domain;
using Services.Common.Exceptions;
using Services.Common.Extensions;
using Uri = System.Uri;

namespace Services.Business.Domain
{
    public class Business : IBusiness
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string TradingName { get; private set; }
        public int Code { get; private set; }

        public Uri WebAddress { get; private set; }
        public HeadOffice Office { get; private set; }
        public HeadContact Contact { get; private set; }

        public IBusiness Setup(string name, string tradingName, string webAddress, IHeadOffice headOffice, IHeadContact headContact, int code)
        {
            Validate(name, tradingName, webAddress, code);

            Id = new Guid();
            Name = name;
            TradingName = tradingName;
            Office = headOffice as HeadOffice;
            Contact = headContact as HeadContact;
            Code = code;

            return this;
        }


        public void Update(string name, string tradingName, string webAddress)
        {
            Validate(name, tradingName, webAddress, Code);
            Name = name;
            TradingName = tradingName;
        }

        public IHeadOffice GetOffice() => Office;

        public IHeadContact GetContact() => Contact;

        private void Validate(string name, string tradingName, string webAddress, int code)
        {
            if (code.ToString().ToCharArray().Length != 6) throw new VmsException(Codes.InvalidId,
                    "A unique code could not be generated for this business at this time.");
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
        }
    }
}
