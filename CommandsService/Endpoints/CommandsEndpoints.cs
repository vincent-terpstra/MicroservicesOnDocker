using CommandsService.Data;
using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Endpoints;

public static class CommandsEndpoints
{
    public static void MapCommandRoutes(this IEndpointRouteBuilder builder)
    {
        var commandRoutes = builder.MapGroup("api/c/commands");
        commandRoutes.MapGet("", GetAllCommandsAsync);
        commandRoutes.MapGet("{id}", GetCommandByIdAsync);
        
        var platformroutes =  builder.MapGroup("api/c/platforms");
        platformroutes.MapGet("{platformId}", GetCommandsByPlatformIdAsync);
        platformroutes.MapGet("{platformId}/commands/{commandId}", GetCommandByPlatformAndCommandIdAsync);
        platformroutes.MapPost("{platformId}/commands", CreateCommandForPlatformAsync);
    }

    private static Task CreateCommandForPlatformAsync(CommandsDbContext context, Command command, int platformId, CancellationToken ct)
    {
        command.PlatformId = platformId;
        context.Commands.Add(command);
        return context.SaveChangesAsync(ct);
    }

    private static Task GetCommandByPlatformAndCommandIdAsync(CommandsDbContext context, int platformId, int commandId)
        => context.Commands.Where(command => command.Id == commandId && command.PlatformId == platformId).FirstAsync();

    static Task<List<Command>> GetAllCommandsAsync(CommandsDbContext dbContext, CancellationToken ct)
        => dbContext.Commands.ToListAsync(ct);

    static Task<Command> GetCommandByIdAsync(CommandsDbContext dbContext, long id, CancellationToken ct)
        => dbContext.Commands.FirstAsync(c => c.Id == id, ct);
    
    static Task<List<Command>> GetCommandsByPlatformIdAsync(CommandsDbContext dbContext, int platformId, CancellationToken ct)
        => dbContext.Commands.Where(c => c.PlatformId == platformId).ToListAsync(ct);
}