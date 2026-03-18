using CommandsService.Data;
using CommandsService.Events;
using CommandsService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.EventHandlers;

public class PlatformPublishedEventHandler(CommandsDbContext context) : IEventHandler<PlatformPublishedEvent>
{
    public async Task HandleAsync(PlatformPublishedEvent @event, CancellationToken ct)
    {
        var platform = @event.ToPlatformModel();
        bool exists = await context.Platforms.AnyAsync(x => x.ExternalId == platform.ExternalId, ct);
        if(exists)
            return;
        
        context.Platforms.Add(platform);
        await context.SaveChangesAsync(ct);
    }
}