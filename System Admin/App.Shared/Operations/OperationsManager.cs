using App.Shared.Exceptions;
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
        private readonly IOperationsClient _operationsClient;
        public List<IOperationMessage> Messages { get; private set; }

        public OperationsManager(IOperationsService operationsService, ILogger<OperationsManager> logger, IOperationsClient operationsClient)
        {
            Messages = new List<IOperationMessage>();            
            _logger = logger;
            _operationsClient = operationsClient;
            operationsService.ConnectionStatusUpdated += ConnectionStatusUpdatedHandler;
            operationsService.MessageReceived += MessageRecievedHandler;
        }

        public bool IsConnected { get; set; }

        public async Task<IOperationMessage> GetOperationStatusAsync(Guid id)
        {
            if(!Messages.Any(m => m.Id == id))
            {
                IOperationMessage message = null; ;
                await Task.Run( async () =>
                {
                    int counter = 0;
                    //TODO: Possibly use a timer here may underload fail and always fallback.
                    while (!Messages.Any(m => m.Id == id))
                    {
                        if(counter == 5)
                        {
                            break;
                        }
                        counter++;
                        Thread.Sleep(300);
                    }
                    if(Messages.Any(m => m.Id == id))
                    {
                        message = PopMessage(id, counter);
                        return;
                    }
                    else
                    {
                        message = await _operationsClient.GetOperationMessageAsync(id);
                    }
                });

               

                return message;
            }

            return PopMessage(id);
        }

        private void MessageRecievedHandler(object sender, IOperationMessage message)
        {
            _logger.LogInformation("Message recived with status: " + message.Status);
            Messages.Add(message);
        }
        
        private void ConnectionStatusUpdatedHandler(object sender, ConnectionStatusUpdatedEventArgs args)
        {
            if (args.IsConnected) _logger.LogInformation("Connection made to push service");
            else _logger.LogWarning("Connection lost to push service because " + args.Error + " falling back to api.");

            IsConnected = args.IsConnected;
        }

        IOperationMessage PopMessage(Guid id, int waits = 0)
        {
            var message = Messages.FirstOrDefault(m => m.Id == id);
            _logger.LogInformation($"Message {message.Id} processed " + ((waits == 0) ? "first time" : $"after {waits} attempts"));
            Messages.Remove(message);
            return message;
        }
    }
}
