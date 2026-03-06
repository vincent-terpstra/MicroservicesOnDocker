using CommandsService.Models;

namespace CommandsService.Data;

public static class SeedCommandData
{
    public static void InitializeDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<CommandsDbContext>();
        SeedData(context);
    }

    private static void SeedData(CommandsDbContext context)
    {
        context.Database.EnsureCreated();
        
        context.Commands.AddRange(
            Command.Create("Hello"),
            Command.Create("World")
        );
        context.SaveChanges();
    }
}