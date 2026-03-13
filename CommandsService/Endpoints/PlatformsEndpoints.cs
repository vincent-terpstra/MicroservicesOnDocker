using CommandsService.Data;
using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Endpoints;

public static class PlatformsEndpoints
{
    public static void MapPlatformRoutes(this IEndpointRouteBuilder builder)
    {
        var platformsRoutes = builder.MapGroup("api/c/platforms");
        platformsRoutes.MapPost("", RegisterFromPlatformService);
        platformsRoutes.MapGet("", GetAllPlatformsAsync);

        // support initial route used by platform service
        builder.MapPost("api/platforms", RegisterFromPlatformService);

    }

    private static async Task<IResult> RegisterFromPlatformService(Platform platform, CommandsDbContext dbContext, CancellationToken ct)
    {
        Console.WriteLine($"Platform Service Registered, {platform.Name}");
        dbContext.Platforms.Add(platform);
        await dbContext.SaveChangesAsync(ct);
        return Results.Created("api/c/platforms", platform);
    }

    private static Task<List<Platform>> GetAllPlatformsAsync(CommandsDbContext dbContext, CancellationToken ct)
        => dbContext.Platforms.ToListAsync(ct);
}