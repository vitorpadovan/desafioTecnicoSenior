using Challenge.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace Challenge.Common.Implementation
{
    public class RabbitMqService : IMessageService, IAsyncDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RabbitMqService> _logger;
        private readonly IConnection _connection;
        private readonly ConcurrentBag<IChannel> _channelPool = new();
        private readonly SemaphoreSlim _channelPoolSemaphore = new(1, 10);

        public RabbitMqService(ILogger<RabbitMqService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("RabbitMq");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "A string de conexão não pode ser nula ou vazia.");
            }

            var factory = new ConnectionFactory
            {
                Uri = new Uri(connectionString)
            };

            _connection = factory.CreateConnectionAsync().Result;
        }

        private async Task<IChannel> GetOrCreateChannelAsync()
        {
            await _channelPoolSemaphore.WaitAsync();
            try
            {
                if (_channelPool.TryTake(out var channel) && channel.IsOpen)
                {
                    return channel;
                }

                return await _connection.CreateChannelAsync();
            }
            finally
            {
                _channelPoolSemaphore.Release();
            }
        }

        public async Task PublishAsync<T>(T message, string queue = "default", string routingKey = "default", bool forceRoutingKey = false)
        {
            var channel = await GetOrCreateChannelAsync();

            try
            {
                await channel.QueueDeclareAsync(
                    queue: queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: forceRoutingKey ? routingKey : queue,
                    mandatory: true,
                    body: body
                );

                //_logger.LogInformation("Mensagem publicada com sucesso na fila '{Queue}'", queue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao publicar na fila '{Queue}'", queue);
                throw;
            }
            finally
            {
                if (channel.IsOpen)
                {
                    _channelPool.Add(channel);
                }
                else
                {
                    await channel.CloseAsync();
                    await channel.DisposeAsync();
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            foreach (var channel in _channelPool)
            {
                await channel.CloseAsync();
                await channel.DisposeAsync();
            }

            await _connection.CloseAsync();
            _connection.Dispose();
        }

        public async Task InitQueueAsync(string queue = "default")
        {
            var channel = await GetOrCreateChannelAsync();

            try
            {
                await channel.QueueDeclareAsync(
                    queue: queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                _logger.LogInformation("Fila '{Queue}' criada com sucesso.", queue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar a fila '{Queue}'", queue);
                throw;
            }
            finally
            {
                if (channel.IsOpen)
                {
                    _channelPool.Add(channel);
                }
                else
                {
                    await channel.CloseAsync();
                    await channel.DisposeAsync();
                }
            }
        }
    }
}
