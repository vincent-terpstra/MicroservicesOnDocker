namespace CommandsService.Interfaces;

public interface IEventProcessor
{
    Task ProcessEventAsync(string eventName, CancellationToken cancellationToken);
}