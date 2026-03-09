using CommandsService.Models;

namespace CommandsService.Endpoints;

public static class PlatformsEndpoints
{
    public static void MapPlatformRoutes(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("", RegisterFromPlatformService);

    }

    private static Task RegisterFromPlatformService(HttpContext context, Platform platform)
    {
        Console.WriteLine($"Platform Service Registered, {platform.Name}");
        return Task.CompletedTask;
    }
}