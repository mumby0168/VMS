using System;
using SignalRClient.Interfaces;

namespace SignalRClient.Models
{
    public class OperationMessage : IOperationMessage
    {
        public Guid Id { get; }

        public OperationStatus Status { get; }

        public OperationMessage(Guid id, OperationStatus status)
        {
            Id = id;
            Status = status;
        }
    }
}