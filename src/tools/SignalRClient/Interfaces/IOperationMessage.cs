using System;
namespace SignalRClient.Interfaces
{
    public interface IOperationMessage
    {
        Guid Id { get; }

        OperationStatus Status { get; }        
    }
}