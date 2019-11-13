using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Services.Common.Exceptions;
using Services.Common.Jwt;
using Services.Push.Clients;

namespace Services.Push.Hubs
{
    public class VmsHub : Hub
    {
        private readonly ILogger<VmsHub> _logger;
        private readonly ITokensClient _client;

        public VmsHub(ILogger<VmsHub> logger, ITokensClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task Connect(string jwt)
        {
            try
            {
                if (!await _client.IsTokenValid(jwt))
                {
                    await Failed("The token was invalid");
                    return;
                }
            }
            catch (VmsServiceDownException e)
            {
                await Failed("The service: " + e.Service + " appears to be down.");
                return;
            }

            var token = JwtToken.Process(raw: jwt);
            var id = token.Id;
            if (id == Guid.Empty) await Failed("The id provided through the token was empty");
            await Success(id);
        }

        private async Task Success(Guid id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"users:{id.ToString()}");
            await Clients.Client(Context.ConnectionId).SendAsync("connectionSucceeded");
            _logger.LogInformation("User connected successfully with Id: " + id);
        }

        private async Task Failed(string reason)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("connectionFailed", reason);
            _logger.LogWarning("Connection refused for reason: " + reason);
        }
    }
}
