using System.Collections.Generic;
using System;
using SignalRClient.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRClient.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SignalRClient.Services
{
    public class OperationService : IOperationService
    {
        private readonly IServiceProvider _serviceProvider;

        //TODO: Add a dictionary so subs can be removed probably wasting memory.

        private HubConnection _connection;
        private List<Action<IOperationMessage>> _successCallbacks;
        private List<Action<IOperationFailed>> _failedCallbacks;
        private readonly ILogger<OperationService> _logger;
        private readonly ITokenStore _tokenStore;

        public OperationService(IServiceProvider serviceProvider, ILogger<OperationService> logger, ITokenStore tokenStore)
        {
            this._tokenStore = tokenStore;
            this._logger = logger;
            _successCallbacks = new List<Action<IOperationMessage>>();
            _failedCallbacks = new List<Action<IOperationFailed>>();
            _serviceProvider = serviceProvider;
        }

        public event StatusUpdate ConnectionStatusUpdated;

        public async Task CreateConnection(string url)
        {
            _connection = new HubConnectionBuilder().WithUrl(url).Build();

            _connection.Closed += async (exceptioon) =>
            {
                _logger.LogWarning("Connection lost, retrying for reason: " + exceptioon.Message);
                ConnectionStatusUpdated.Invoke(false);
                await Connect();
            };

            _connection.On<Success>("operationComplete", success =>
            {
                var operation = new OperationMessage(success.OperationId, success.Status);
                _successCallbacks.ForEach(callback => callback.Invoke(operation));
            });

            _connection.On<Failure>("operationFailed", failure =>
            {
                var operation = new OperationFailed(failure.OperationId, failure.Status, failure.Reason, failure.Code);
                _failedCallbacks.ForEach(callback => callback.Invoke(operation));
            });

            _connection.On("connectionSucceeded", () => 
            {
                _logger.LogInformation("connection and verification successful.");
                ConnectionStatusUpdated?.Invoke(true);
            });

            _connection.On<string>("connectionFailed", (reason) =>
            {
                ConnectionStatusUpdated?.Invoke(false);
                _logger.LogWarning("Connection failed for reason: " + reason);
            });

            await Connect();

        }

        private async Task Connect()
        {
            try
            {
                await _connection.StartAsync();
                _logger.LogInformation("Starting initial connection");
            }
            catch (Exception e)
            {
                _logger.LogWarning($"connnection failed with exception with the message {e.Message} retrying ...");
                await _connection.StartAsync();
            }            

            await _connection.SendAsync("connect", _tokenStore.Token);
        }

        public void SubscribeOperationFailed(Action<IOperationFailed> callback)
        {
            _failedCallbacks.Add(callback);
        }

        public void SubscribeOperationSuccess(Action<IOperationMessage> callback)
        {
            _successCallbacks.Add(callback);
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