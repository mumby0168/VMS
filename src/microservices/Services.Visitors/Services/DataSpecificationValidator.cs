namespace Services.Visitors.Services
{
    public class DataSpecificationValidator : IDataSpecificationValidator
    {
        public bool IsDataValid(string value, string code)
        {
            //TODO: find regex for these last two.
            switch (code)
            {
                case "Required":
                    return !string.IsNullOrEmpty(value);
                case "Email":
                    return true;
                case "Post Code":
                    return true;
            }

            return false;
        }
    }
}