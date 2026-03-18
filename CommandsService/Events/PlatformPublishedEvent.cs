using CommandsService.Models;

namespace CommandsService.Events;

public class PlatformPublishedEvent
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string EventType { get; set; }
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