using System;
using SignalRClient.Interfaces;

namespace SignalRClient.Models
{
    public class OperationFailed : OperationMessage, IOperationFailed
    {
        public OperationFailed(Guid id, OperationStatus status, string reason, string code) : base(id, status)
        {
            Reason = reason;
            Code = code;
        }

        public string Reason { get; }

        public string Code { get; }
    }
}