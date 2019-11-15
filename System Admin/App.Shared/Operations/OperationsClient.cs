using App.Shared.Operations.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Operations
{
    public class OperationsClient : IOperationsClient
    {
        public HttpClient Client { get; } 
        public OperationsClient(HttpClient client)
        {            
            Client = client;
        }        

        public Task<IOperationMessage> GetOperationMessageAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
