using System.Text;
using System.Text.Json;
using PlatformService.Events;
using PlatformService.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PlatformService.Services.MessageBus;

public class MessageBusClient : IPlatformPublisher, IAsyncDisposable
{
    private readonly ILogger<MessageBusClient> _logger;
    private readonly ConnectionFactory _factory;
    private IConnection _connection = null!;
    private IChannel _channel = null!;

    public MessageBusClient(IConfiguration configuration, ILogger<MessageBusClient> logger)
    {
        _logger = logger;
        string host = configuration.GetValue<string>("RabbitMq:Host")
            ?? throw new ArgumentException("RabbitMq:Host is required.");
        int port = configuration.GetValue<int?>("RabbitMq:Port")
            ?? throw new ArgumentException("RabbitMq:Port is required.");

        _factory = new ConnectionFactory()
        {
            HostName = host,
            Port = port
        };

    }
    
    public async Task PublishAsync(PlatformPublishedEvent platform)
    {
        await InitializeAsync();
        var message = JsonSerializer.Serialize(platform);
        if (_connection.IsOpen)
        {
            // send the message
            var body = Encoding.UTF8.GetBytes(message);
            await _channel.BasicPublishAsync("platforms", routingKey: string.Empty, body);
            _logger.LogInformation("Message published");
        }
            
    }

    private bool _isInitialized = false;
    private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
    
    async Task InitializeAsync()
    {
        if (_isInitialized)
            return;
        
        await _semaphoreSlim.WaitAsync();
        try
        {
            if (_isInitialized)
                return;

            _connection = await _factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            await _channel.ExchangeDeclareAsync(exchange: "platforms", type: ExchangeType.Fanout);
            _connection.ConnectionShutdownAsync += OnConnectionShutdownAsync;
            _logger.LogInformation("Rabbit MQ Connection established");
            _isInitialized = true;

        }
        catch (Exception ex)
        {
           _logger.LogError(ex, "Rabbit MQ connection failed");
        }
    }

    private Task OnConnectionShutdownAsync(object sender, ShutdownEventArgs @event)
    {
        _logger.LogInformation("Rabbit MQ connection shutdown");
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _channel.DisposeAsync();
        _semaphoreSlim.Dispose();
    }
}