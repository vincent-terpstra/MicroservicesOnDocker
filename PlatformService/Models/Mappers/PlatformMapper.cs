using PlatformService.Request;
using PlatformService.Response;
using Riok.Mapperly.Abstractions;

namespace PlatformService.Models.Mappers;

[Mapper]
public static partial class PlatformMapper
{
    [MapperIgnoreSource(nameof(Platform.Cost))]
    [MapperIgnoreSource(nameof(Platform.Description))]
    public static partial GetPlatformResponse ToResponseModel(this Platform platform);
    
    
    [MapperIgnoreTarget(nameof(Platform.Id))]
    public static partial Platform ToDomainModel(this CreatePlatformRequest platform);
    
    public static partial void Update(this UpdatePlatformRequest platform, Platform platformToUpdate);
}