using App.Shared.Operations.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Shared.Operations
{
    public class OperationsManager : IOperationsManager
    {        
        private readonly ILogger<OperationsManager> _logger;
        private readonly OperationsClient _operationsClient;
        private List<IOperationMessage> _messages;

        public OperationsManager(IOperationsService operationsService, ILogger<OperationsManager> logger, OperationsClient operationsClient)
        {
            _messages = new List<IOperationMessage>();            
            _logger = logger;
            _operationsClient = operationsClient;
            operationsService.ConnectionStatusUpdated += ConnectionStatusUpdatedHandler;
            operationsService.MessageReceived += MessageRecievedHandler;
        }

        public bool IsConnected { get; set; }

        public async Task<IOperationMessage> GetOperationStatus(Guid id)
        {
            if(!_messages.Any(m => m.Id == id))
            {
                IOperationMessage message = null; ;
                await Task.Run( async () =>
                {                    
                    Thread.Sleep(300);
                    if(_messages.Any(m => m.Id == id))
                    {
                        message = PopMessage(id);
                        return;
                    }
                    else
                    {
                        message = await _operationsClient.GetOperationMessageAsync(id);
                    }
                });

                if(message is null)
                {
                    //throw something.
                }

                return message;
            }

            return PopMessage(id);
        }

        private void MessageRecievedHandler(object sender, IOperationMessage message)
        {
            _logger.LogInformation("Message recived with status: " + message.Status);
            _messages.Add(message);
        }
        
        private void ConnectionStatusUpdatedHandler(object sender, ConnectionStatusUpdatedEventArgs args)
        {
            if (args.IsConnected) _logger.LogInformation("Connection made to push service");
            else _logger.LogWarning("Connection lost to push service because " + args.Error + " falling back to api.");

            IsConnected = args.IsConnected;
        }

        IOperationMessage PopMessage(Guid id)
        {
            var message = _messages.FirstOrDefault(m => m.Id == id);
            _logger.LogInformation($"Message {message.Id} processed.");
            _messages.Remove(message);
            return message;
        }
    }
}
