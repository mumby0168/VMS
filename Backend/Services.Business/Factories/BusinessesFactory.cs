using Services.Business.Domain;

namespace Services.Business.Factories
{
    public class BusinessesFactory : IBusinessesFactory
    {
        public IBusiness CreateBusiness(string name, string tradingName, string webAddress, string headContactFirstName,
            string headContactSecondName, string headContactContactNumber, string headContactEmail, string headOfficePostCode,
            string headOfficeAddressLine1, string headOfficeAddressLine2, int code)
        {
            var headOffice = new HeadOffice().Setup(headOfficePostCode, headOfficeAddressLine1, headOfficeAddressLine2);
            var headContact = new HeadContact().Setup(headContactFirstName, headContactSecondName, headContactContactNumber, headContactEmail);
            return new Domain.Business().Setup(name, tradingName, webAddress, headOffice, headContact, code);
        }
    }
}
