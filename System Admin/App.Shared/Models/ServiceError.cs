using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

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

        public static ServiceError Standard => new ServiceError(string.Empty, "The service may be down. Please try again later.");

        public async static Task<ServiceError> Deserialize(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ServiceError>(json);
            return res;
        }
    }
}