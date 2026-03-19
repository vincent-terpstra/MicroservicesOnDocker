using CommandsService.Interfaces;
using Newtonsoft.Json;

namespace CommandsService.Services;

public class EventProcessor(IServiceScopeFactory scopeFactory) : IEventProcessor
{
    public async Task ProcessEventAsync(string message, CancellationToken ct)
    {
        object @event = JsonConvert.DeserializeObject<object>(message, new JsonSerializerSettings()
        {
            Converters = { new EventConverter() }
        })!;
        
        var eventType = @event.GetType();

        var method = typeof(EventProcessor)
            .GetMethod(nameof(ProcessAsync))!
            .MakeGenericMethod(eventType);

        var task = (Task)method.Invoke(this, [@event, ct])!;
        await task;
    }

    public async Task ProcessAsync<T>(T message, CancellationToken ct)
    {
        using var scope = scopeFactory.CreateScope();
        var processor = scope.ServiceProvider.GetService<IEventHandler<T>>()
            ?? throw new InvalidOperationException("EventHandler not found.");
        
        await processor.HandleAsync(message, ct);
    }
}


