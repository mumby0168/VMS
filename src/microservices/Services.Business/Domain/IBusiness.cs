using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Common.Domain;

namespace Services.Business.Domain
{
    public interface IBusiness : IDomain
    {

        IBusiness Setup(string name, string tradingName, string webAddress, IHeadOffice headOffice, IHeadContact headContact, int code);

        void Update(string name, string tradingName, string webAddress);

        IHeadContact GetContact();

        IHeadOffice GetOffice();

        string Name { get; }

        string TradingName { get; }

        int Code { get; }

        Uri WebAddress { get; }


        HeadOffice Office { get; }

        HeadContact Contact { get; }
    }
}
