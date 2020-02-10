namespace SignalRClient.Interfaces
{
    public interface IOperationFailed : IOperationMessage
    {
         string Reason { get; }

         string Code { get; }
    }
}