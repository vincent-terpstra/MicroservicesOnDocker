using System.Text;
using CommandsService.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandsService.Services;

public class MessageBusSubscriber: BackgroundService, IAsyncDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _processor;
    private IConnection _connection;
    private IChannel _channel;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor processor)
    {
        _configuration = configuration;
        _processor = processor;
    }

    private async Task InitializeMessageBusAsync()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMq:Host"] ?? throw new ArgumentException("RabbitMq:Host"),
            Port = int.Parse(_configuration["RabbitMq:Port"] ?? throw new ArgumentException("RabbitMq:Port")),
        };
        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();
        await _channel.ExchangeDeclareAsync( exchange: "platforms", type: ExchangeType.Fanout);
        await _channel.QueueDeclareAsync("commands_queue", true, false, false, null);
        await _channel.QueueBindAsync(queue: "commands_queue", exchange: "platforms", routingKey: "");
        _connection.ConnectionShutdownAsync += OnShutdownAsync;
    }

    private Task OnShutdownAsync(object sender, ShutdownEventArgs @event)
    {
        Console.WriteLine($"RabbitMQ connection shutdown: {@event.ReplyText}");
        return Task.CompletedTask;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        await InitializeMessageBusAsync();
        
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (handle, eventArgs) =>
        {
            Console.WriteLine($"RabbitMQ message received: {eventArgs.Body}");
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            await _processor.ProcessEventAsync(message, eventArgs.CancellationToken);
        };
        
        await _channel.BasicConsumeAsync(
            queue: "commands_queue",
            autoAck: true,
            consumer: consumer, 
            stoppingToken);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        await _connection.DisposeAsync();
        await _channel.DisposeAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }
}