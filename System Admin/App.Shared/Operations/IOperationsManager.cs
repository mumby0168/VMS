using App.Shared.Operations.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Operations
{
    public interface IOperationsManager
    {
        bool IsConnected { get; set; }

        Task<IOperationMessage> GetOperationStatus(Guid id);

        List<IOperationMessage> Messages { get; }
    }
}
