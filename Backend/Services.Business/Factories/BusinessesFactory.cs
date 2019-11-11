using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Businesses.Domain;

namespace Services.Businesses.Factories
{
    public class BusinessesFactory : IBusinessesFactory
    {
        public Business CreateBusiness(string name, string tradingName, string webAddress, string headContactFirstName,
            string headContactSecondName, string headContactContactNumber, string headContactEmail, string headOfficePostCode,
            string headOfficeAddressLine1, string headOfficeAddressLine2)
        {
            return new Business(name, tradingName, webAddress,
                new HeadOffice(headOfficePostCode, headOfficeAddressLine1,
                   headOfficeAddressLine2),
                new HeadContact(headContactFirstName, headContactSecondName,
                   headContactContactNumber, headContactEmail));
        }
    }
}
