namespace PlatformService.Request;

public class CreatePlatformRequest
{
    public required string Name { get; set; }
    public required string Publisher { get; set; }
    public required string Description { get; set; }
    public required string Cost { get; set; }
}