using System;
using System.Collections.Generic;
using System.Text;

namespace App.Shared.Operations.Models
{
    public class OperationMessage : IOperationMessage
    {
        public Guid Id { get; set; }

        public OperationStatus Status { get; set; }

        public OperationMessage(Guid id, OperationStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}
