using Grpc.Core;
using PlatformService.Data.Interfaces;
using PlatformService.Models.Mappers;

namespace PlatformService.Services.Grpc;

public class GrpcPlatformService(IPlatformRepo repository): GrpcPlatform.GrpcPlatformBase
{
    public override async Task<PlatformResponse> GetAllPlatforms(GetAllPlatformsRequest request, ServerCallContext context)
    {
        var platforms = await repository.GetAllPlatformsAsync();
        var response = new PlatformResponse();
        response.Platforms.AddRange(platforms.Select(p => p!.ToGrpcModel()));
        
        return response;
    }
}