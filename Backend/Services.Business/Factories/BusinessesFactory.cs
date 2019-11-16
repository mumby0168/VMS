using Services.Business.Domain;

namespace Services.Business.Factories
{
    public class BusinessesFactory : IBusinessesFactory
    {
        public Domain.Business CreateBusiness(string name, string tradingName, string webAddress, string headContactFirstName,
            string headContactSecondName, string headContactContactNumber, string headContactEmail, string headOfficePostCode,
            string headOfficeAddressLine1, string headOfficeAddressLine2)
        {
            return new Domain.Business(name, tradingName, webAddress,
                new HeadOffice(headOfficePostCode, headOfficeAddressLine1,
                   headOfficeAddressLine2),
                new HeadContact(headContactFirstName, headContactSecondName,
                   headContactContactNumber, headContactEmail));
        }
    }
}
