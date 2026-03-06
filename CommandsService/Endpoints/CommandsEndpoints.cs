using CommandsService.Data;
using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Endpoints;

public static class CommandsEndpoints
{
    public static void MapCommandRoutes(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("", GetAllCommandsAsync);
        builder.MapGet("{id}", GetCommandByIdAsync);
    }

    static Task<List<Command>> GetAllCommandsAsync(CommandsDbContext dbContext, CancellationToken ct)
        => dbContext.Commands.ToListAsync(ct);
    
    static Task<Command> GetCommandByIdAsync(CommandsDbContext dbContext, long id, CancellationToken ct)
        => dbContext.Commands.FirstAsync(c => c.Id == id, ct);

}