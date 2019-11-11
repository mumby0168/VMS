using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Businesses.Domain;

namespace Services.Businesses.Factories
{
    public interface IBusinessesFactory
    {
        Business CreateBusiness(string name, string tradingName, string webAddress, string headContactFirstName, string headContactSecondName, string headContactContactNumber, string headContactEmail, string headOfficePostCode, string headOfficeAddressLine1, string headOfficeAddressLine2);
    }
}
