using CommandsService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandsService.Services.Grpc;

public interface IPlatformDataClient
{
    Task<IEnumerable<Platform>> GetPlatformsAsync();
}

public class PlatformDataClient(IConfiguration configuration, ILogger<PlatformDataClient> _logger) : IPlatformDataClient
{
    public async Task<IEnumerable<Platform>> GetPlatformsAsync()
    {
        string grpcroute = configuration.GetValue<string>("GrpcPlatform") ??
                throw new ArgumentException("GrpcPlatform is required in config.");
        
        var channel = GrpcChannel.ForAddress(grpcroute);
        var client = new GrpcPlatform.GrpcPlatformClient(channel);
        
        var request = new GetAllPlatformsRequest();

        try
        {
            var response = await client.GetAllPlatformsAsync(request);
            return response.Platforms.Select(p=> new Platform()
            {
                ExternalId = p.PlatformId,
                Name = p.Name,
                Id = p.PlatformId
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to get all platforms");
            return [];
        }
    }
}