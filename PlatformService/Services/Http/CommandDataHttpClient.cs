using PlatformService.Response;
using PlatformService.Services.Http.Interfaces;

namespace PlatformService.Services.Http;

public class CommandDataHttpClient(HttpClient httpClient) : ICommandDataClient
{
    public async Task SendToCommandServiceAsync(PlatformResponse platform)
    {
        var response =  await httpClient.PostAsJsonAsync( "api/platforms", platform);
        response.EnsureSuccessStatusCode();
    }
}