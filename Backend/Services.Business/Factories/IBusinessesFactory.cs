namespace Services.Business.Factories
{
    public interface IBusinessesFactory
    {
        Domain.Business CreateBusiness(string name, string tradingName, string webAddress, string headContactFirstName, string headContactSecondName, string headContactContactNumber, string headContactEmail, string headOfficePostCode, string headOfficeAddressLine1, string headOfficeAddressLine2);
    }
}
