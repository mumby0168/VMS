using App.Shared.Context;
using App.Shared.Events;
using App.Shared.Operations.Models;
using App.Shared.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Operations
{
    public class OperationsService : IOperationsService
    {
        public event EventHandler<ConnectionStatusUpdatedEventArgs> ConnectionStatusUpdated;
        public event EventHandler<IOperationMessage> MessageReceived;

        private const string HubUrl = "http://localhost:5015/operations";
        private readonly ILogger<OperationsService> _logger;
        private readonly IUserContext _userContext;
        private HubConnection _connection;

        public OperationsService(ILogger<OperationsService> logger, IUserContext userContext, IPubSubService pubSubService)
        {
            _logger = logger;
            _userContext = userContext;
            pubSubService.Subscribe<LoginSuccesfulEvent>(LoginSuccesful);
        }

        private async Task LoginSuccesful()
        {
            await CreateConnection();
        }

        public async Task CreateConnection()
        {
            _connection = new HubConnectionBuilder().WithUrl(HubUrl).Build();

            _connection.Closed += async (exception) =>
           {
               _logger.LogWarning("Disconnected attempting to reconnect");
               await Connect();
           };

            await Connect();

            _connection.On("connectionSucceeded", () =>
            {
                ConnectionStatusUpdated?.Invoke(this, new ConnectionStatusUpdatedEventArgs(true));
                _logger.LogInformation("Connection accepted");
            });

            _connection.On<string>("connectionFailed", (err) => 
            {
                ConnectionStatusUpdated?.Invoke(this, new ConnectionStatusUpdatedEventArgs(false, err));
                _logger.LogWarning("Connection refused for reason: " + err);
            });

            _connection.On<Success>("operationComplete", success =>
            {
                var operation = new OperationMessage(success.OperationId, success.Status);
                MessageReceived?.Invoke(this, operation);
            });

            _connection.On<Failure>("operationFailed", failure =>
            {
                var operation = new OperationMessageFailed(failure.OperationId, failure.Status, failure.Reason, failure.Code);
                MessageReceived?.Invoke(this, operation);
            });
        }

        private async Task Connect()
        {
            try
            {
                _logger.LogInformation("Starting initial connection");
                await _connection.StartAsync();
            }
            catch (Exception)
            {
                _logger.LogWarning("Inital connection failed retrying");
                await _connection.StartAsync();
                throw;
            }

            await _connection.SendAsync("connect", _userContext.Token);
        }



        private class Success
        {
            public Guid OperationId { get; set; }

            public OperationStatus Status { get; set; }

        }

        private class Failure
        {
            public Guid OperationId { get; set; }

            public OperationStatus Status { get; set; }

            public string Code { get; set; }

            public string Reason { get; set; }
        }
    }
}
