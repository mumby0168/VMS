namespace App.Shared.Models
{
    public class ServiceError
    {
        public ServiceError(string code, string reason)
        {
            this.Code = code;
            this.Reason = reason;

        }
        public string Code { get; }

        public string Reason { get; }
    }
}