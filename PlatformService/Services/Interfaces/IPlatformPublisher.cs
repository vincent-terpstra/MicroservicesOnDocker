using PlatformService.Events;

namespace PlatformService.Services.Interfaces;

public interface IPlatformPublisher
{
    Task PublishAsync(PlatformPublishedEvent platform);
}