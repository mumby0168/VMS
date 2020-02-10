using App.Shared.Operations.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Operations
{
    public interface IOperationsClient
    {
        Task<IOperationMessage> GetOperationMessageAsync(Guid id);
    }
}
