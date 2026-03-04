using Microsoft.EntityFrameworkCore;
using PlatformService.Data.Interfaces;
using PlatformService.Models;

namespace PlatformService.Data.Repositories;

public class PlatformRepo(AppDbContext context) : IPlatformRepo
{
    private DbSet<Platform> Platforms => context.Platforms;

    public Task<int> SaveChangesAsync()
        => context.SaveChangesAsync();
    
    public async Task<IEnumerable<Platform?>> GetAllPlatformsAsync()
        => await Platforms.ToListAsync();

    public async Task<Platform?> GetPlatformById(int id)
        => await Platforms.FindAsync(id);

    public Platform AddPlatform(Platform platform)
    { 
        ArgumentNullException.ThrowIfNull(platform);
        
        Platforms.Add(platform);
        return platform;
    }

    public async Task<bool> DeletePlatformAsync(int id)
    {
        var platform = await Platforms.FindAsync(id);
        if (platform == null)
            return false;
        Platforms.Remove(platform);
        return await SaveChangesAsync() > 0;
    }
}