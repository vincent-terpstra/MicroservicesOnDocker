namespace CommandsService.Interfaces;

public interface IEventHandler<in T>
{
    public Task HandleAsync(T command, CancellationToken ct);
}