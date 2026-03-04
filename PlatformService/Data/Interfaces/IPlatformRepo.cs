using PlatformService.Models;

namespace PlatformService.Data.Interfaces;

public interface IPlatformRepo
{
    Task<int> SaveChangesAsync();
    
    Task<IEnumerable<Platform?>> GetAllPlatformsAsync();
    
    Task<Platform?> GetPlatformById(int id);
    
    Platform AddPlatform(Platform platform);

    Task<bool> DeletePlatformAsync(int id);
}