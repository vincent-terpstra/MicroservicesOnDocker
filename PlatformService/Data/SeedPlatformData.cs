using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public static class SeedPlatformData
{
    public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(":memory:"));
        }
        else
        {
            var connectionString = builder.Configuration.GetConnectionString("PlatformsDatabase");
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
                connectionString
            ));
        }

        return builder;
    }

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
        if (context.Platforms.Any())
            return;

        context.Platforms.AddRange(
            Platform.Create("Dot Net", "Microsoft", "Free", "Microsoft"),
            Platform.Create("SQL Server Express", "Microsoft", "Free", "Microsoft"),
            Platform.Create("Kubernetes", "Cloud Native Computing Foundation", "Free", "Microsoft")
        );

        context.SaveChanges();
    }
}