using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Service.SignlarR
{
    public class DemoHub : Hub
    {
        private readonly ILogger<DemoHub> _logger;

        public DemoHub(ILogger<DemoHub> logger)
        {
            _logger = logger;
        }
        private const string ValidCode = "secret";
        public async Task Setup(string code)
        {
            if (code == ValidCode)
            {
                await AddUserGroup(code);
                await ConnectAsync();
                _logger.LogInformation("Connection made with valid code by user with connection id: " + Context.ConnectionId);
            }
            else
            {
                await Disconnect();
                _logger.LogWarning("Connection failed with code: "  + code + "by user with connection id: " + Context.ConnectionId);
            }
        }

        private async Task AddUserGroup(string code)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"users:{code}");
        }

        private async Task ConnectAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("success", "connection successful");
        }

        private async Task Disconnect()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("failed", "connection failed");
        }
    }
}
