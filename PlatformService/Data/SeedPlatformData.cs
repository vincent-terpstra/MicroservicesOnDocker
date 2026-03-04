using PlatformService.Models;

namespace PlatformService.Data;

public static class SeedPlatformData
{
    public static void InitializeDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();
        SeedData(context);
    }

    private static void SeedData(this AppDbContext context)
    {
        context.Database.EnsureCreated();
        if(context.Platforms.Any())
            return;
        
        context.Platforms.AddRange(
            Platform.Create("Dot Net", "Microsoft", "Free", "Microsoft"),
            Platform.Create("SQL Server Express", "Microsoft", "Free", "Microsoft"),
            Platform.Create("Kubernetes", "Cloud Native Computing Foundation", "Free", "Microsoft")
        );
        
        context.SaveChanges();
    }
}