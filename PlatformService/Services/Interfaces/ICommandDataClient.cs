using PlatformService.Response;

namespace PlatformService.Services.Http.Interfaces;

public interface ICommandDataClient
{
    Task SendToCommandServiceAsync(PlatformResponse platform);
}