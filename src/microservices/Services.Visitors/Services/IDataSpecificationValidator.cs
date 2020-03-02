namespace Services.Visitors.Services
{
    public interface IDataSpecificationValidator
    {
        bool IsDataValid(string value, string code);
    }
}