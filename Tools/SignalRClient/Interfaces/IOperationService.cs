using System.Threading.Tasks;
using System;
namespace SignalRClient.Interfaces
{
    public delegate void StatusUpdate(bool connected);
    public interface IOperationService
    {
         Task CreateConnection(string url);

         void SubscribeOperationSuccess(Action<IOperationMessage> callback);

         void SubscribeOperationFailed(Action<IOperationFailed> callback);

         event StatusUpdate ConnectionStatusUpdated;
    }
}