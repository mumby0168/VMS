using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Services.Common.Mongo;
using Services.Logs.Decode;
using Services.Logs.Domain;

namespace Services.Logs.Server
{
    public class UdpServer : IUdpServer
    {
        private readonly ILogDecoder _decoder;
        private readonly IMongoRepository<Log> _repository;
        private readonly ILogger<UdpServer> _logger;
        private readonly CancellationTokenSource _source;
        private readonly CancellationToken _token;
        private readonly IPAddress _address;

        public UdpServer(ILogDecoder decoder, IMongoRepository<Log> repository, ILogger<UdpServer> logger)
        {
            _decoder = decoder;
            _repository = repository;
            _logger = logger;
            _source = new CancellationTokenSource();
            _token = _source.Token;
            _address = IPAddress.Parse("127.0.0.1");
        }

        public void Begin(int port)
        {
            Task.Run(() =>
            {
                //create connection
                var endpoint = new IPEndPoint(_address, port);
                var listener = new UdpClient(endpoint);

                while (!_token.IsCancellationRequested)
                {
                    _logger.LogInformation($"Starting receiving on port: {port}");

                    var bytes = listener.Receive(ref endpoint);

                    _logger.LogInformation($"LogAsync received payload size: {bytes.Length}");

                    Task.Run(async () =>
                    {
                        var log = _decoder.DecodeLog(bytes);
                        if (log is null)
                        {
                            _logger.LogInformation($"LogAsync failed to decode payload size: {bytes.Length}");
                            return;
                        }

                        _logger.LogInformation($"LogAsync received from service: {log.ServiceName}");

                        await _repository.AddAsync(log as Log);

                        _logger.LogInformation($"LogAsync written to store with id: {log.Id}");
                    });
                }

                _logger.LogInformation("Closing udp connection.");
                listener.Close();
                listener.Dispose();
                _logger.LogInformation("Closed udp connection.");

            }, _token);
        }

        public void Stop()
        {
            _source.Cancel();
        }
    }
}
