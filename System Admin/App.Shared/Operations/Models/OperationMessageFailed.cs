using System;
using System.Collections.Generic;
using System.Text;

namespace App.Shared.Operations.Models
{
    public class OperationMessageFailed : IOperationMessageFailed
    {
        public string Code { get; }

        public string Reason { get; }

        public Guid Id { get; }

        public OperationStatus Status { get; }

        public OperationMessageFailed(Guid id, OperationStatus status, string code, string reason)
        {
            Id = id;
            Status = status;
            Code = code;
            Reason = reason;
        }
    }
}
