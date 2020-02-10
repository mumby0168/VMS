using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Services
{
    public interface IHttpExecutor
    {
        Task<bool> SendRequestAsync(Func<Task<HttpResponseMessage>> call, string successMessage, Func<Task> onFailure = null, Func<Task> onSuccess = null);

        Task<T> GetAsync<T>(string url);
    }
}
