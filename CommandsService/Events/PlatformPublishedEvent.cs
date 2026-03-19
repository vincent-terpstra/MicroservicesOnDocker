using CommandsService.Models;

namespace CommandsService.Events;

public class PlatformPublishedEvent
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string EventType { get; set; } = string.Empty;
}

public static class PlatformPublishedEventExtensions
{
    public static Platform ToPlatformModel(this PlatformPublishedEvent platform)
    => new Platform()
    {
        ExternalId = platform.Id,
        Name = platform.Name,
    };
    
}