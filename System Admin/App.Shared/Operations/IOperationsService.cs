using App.Shared.Operations.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Operations
{
    public class ConnectionStatusUpdatedEventArgs
    {        
        public bool IsConnected { get; }

        public string Error { get;}
        public ConnectionStatusUpdatedEventArgs(bool isConnected, string error = null)
        {
            IsConnected = isConnected;
            Error = error;
        }
    }
    public interface IOperationsService
    {
        Task CreateConnection();

        EventHandler<ConnectionStatusUpdatedEventArgs> ConnectionStatusUpdated { get; set; }

        EventHandler<IOperationMessage> MessageReceived { get; set; }
    }
}
