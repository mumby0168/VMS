using System;
using System.Collections.Generic;
using System.Text;

namespace App.Shared.Operations.Models
{
    public interface IOperationMessageFailed : IOperationMessage
    {
        string Code { get; }

        string Reason { get;  }
    }
}
