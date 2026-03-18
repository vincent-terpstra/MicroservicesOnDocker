using PlatformService.Events;
using PlatformService.Request;
using Riok.Mapperly.Abstractions;
namespace PlatformService.Models.Mappers;
[Mapper]
public static partial class PlatformMapper
{
    [MapperIgnoreSource(nameof(Platform.Cost))]
    [MapperIgnoreSource(nameof(Platform.Description))]
    public static partial Response.PlatformResponse ToResponseModel(this Platform platform);
    
    [MapperIgnoreTarget(nameof(Platform.Id))]
    public static partial Platform ToDomainModel(this CreatePlatformRequest platform);
    
    public static partial void Update(this UpdatePlatformRequest platform, Platform platformToUpdate);
    
    [MapperIgnoreSource(nameof(Platform.Publisher))]
    [MapperIgnoreSource(nameof(Platform.Cost))]
    [MapperIgnoreSource(nameof(Platform.Description))]
    [MapperIgnoreTarget(nameof(PlatformPublishedEvent.EventType))]
    public static partial PlatformPublishedEvent ToPublishEvent(this Platform platform);
    public static GrpcPlatformModel ToGrpcModel(this Platform platform)
    {
        return new GrpcPlatformModel
        {
            PlatformId = platform.Id,
            Name = platform.Name,
            Publisher = platform.Publisher
        };
    }

}