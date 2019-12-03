using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Services.Common.Logging
{
    public class UdpLoggingClient : IUdpLoggingClient
    {
        private readonly ServiceLoggingOptions _options;
        private readonly ILogEncoder _encoder;
        private readonly ILogger<UdpLoggingClient> _logger;
        private UdpClient _client;

        public UdpLoggingClient(ServiceLoggingOptions options,ILogEncoder encoder, ILogger<UdpLoggingClient> logger)
        {
            _options = options;
            _encoder = encoder;
            _logger = logger;
        }

        public void CreateConnection()
        {
            _client = new UdpClient(_options.Port);
            _client.Connect(IPAddress.Parse(_options.Address), _options.Port);
            _logger.LogInformation($"Connected to udp logging service at host: {_options.Address} port: {_options.Port}");
            IsConnected = true;
        }

        public bool IsConnected { get; private set; }

        public async Task LogAsync(string category, LogType type, string message, Guid operationId, Guid userId)
        {
            if (!IsConnected) return;
            try
            {
                var bytes = _encoder.EncodeLog(_options.ServiceName, category, type, message, operationId, userId);

                await _client.SendAsync(bytes, bytes.Length);

                _logger.LogInformation("Sent message to external logging");
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to send message with reason: {e.Message}");
            }
        }

        public void Disconnect()
        {
            _client.Close();
            _client.Dispose();
            IsConnected = false;
        }
    }
}
