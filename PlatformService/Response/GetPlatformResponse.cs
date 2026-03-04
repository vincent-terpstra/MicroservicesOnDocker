namespace PlatformService.Response;

public class GetPlatformResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Publisher { get; set; }
}