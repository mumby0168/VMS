using System;
using System.Collections.Generic;
using System.Text;

namespace App.Shared.Operations.Models
{
    public enum OperationStatus : byte
    {
        Pending,
        Failed,
        Complete
    }

    public interface IOperationMessage
    {
        Guid Id { get;  }

        OperationStatus Status { get; }


    }
}
