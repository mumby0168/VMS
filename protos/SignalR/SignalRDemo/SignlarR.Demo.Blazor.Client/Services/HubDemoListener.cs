using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignlarR.Demo.Blazor.Client.Services
{
    public class DemoMessage
    {
        public int Id { get; set; }

        public string Message { get; set; }
    }

    public class HubDemoListener
    {
        private HubConnection _connection;

        public HubDemoListener()
        {
            
        }

        public EventHandler<string> ConnectionStatusEvent { get; set; }

        public EventHandler<string> MessageReceived { get; set; }

        public async Task CreateConnection()
        {
            _connection = new HubConnectionBuilder().WithUrl("http://localhost:51568/demo").Build();

            _connection.On<DemoMessage>("pump", message =>
            {
                MessageReceived?.Invoke(this, message.Id + " message: " + message.Message);
            });

            _connection.On<string>("success", message =>
            {
                MessageReceived?.Invoke(this, message);
            });

            _connection.On<string>("failed", message =>
            {
                MessageReceived?.Invoke(this, message);
            });

            _connection.Closed += async (err) =>
            {
                ConnectionStatusEvent?.Invoke(this, "disconnected with error: " + err.Message);
                await _connection.StartAsync();
                ConnectionStatusEvent?.Invoke(this, "retrying connection");
            };

            try
            {
                await _connection.StartAsync();
                ConnectionStatusEvent?.Invoke(this, "initial connection begun"); 
                await _connection.SendAsync("setup", "secret");
            }
            catch (Exception e)
            {
                ConnectionStatusEvent?.Invoke(this, "disconnected with error: " + e.Message);
                throw;
            }
        }




    }
}
