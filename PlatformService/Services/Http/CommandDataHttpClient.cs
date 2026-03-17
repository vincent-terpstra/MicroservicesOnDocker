using PlatformService.Events;
using PlatformService.Response;
using PlatformService.Services.Interfaces;

namespace PlatformService.Services.Http;

public class CommandDataHttpClient(HttpClient httpClient) : IPlatformPublisher
{
    public async Task PublishAsync(PlatformPublishedEvent platform)
    {
        var response =  await httpClient.PostAsJsonAsync( "api/platforms", platform);
        response.EnsureSuccessStatusCode();
    }
}