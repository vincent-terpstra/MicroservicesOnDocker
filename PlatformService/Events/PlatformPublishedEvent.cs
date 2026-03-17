namespace PlatformService.Events;

public class PlatformPublishedEvent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string EventType { get; set; }
}