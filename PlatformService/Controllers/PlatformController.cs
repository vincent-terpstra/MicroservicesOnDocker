using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Interfaces;
using PlatformService.Models;
using PlatformService.Models.Mappers;
using PlatformService.Request;
using PlatformService.Services.Http.Interfaces;

namespace PlatformService.Controllers;

[ApiController, Route("api/platforms")]
public class PlatformController(
    IPlatformRepo repository,
    ICommandDataClient commands,
    ILogger<PlatformController> logger
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var platforms = await repository.GetAllPlatformsAsync();
        return Ok(platforms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var platform = await repository.GetPlatformById(id);
        return platform == null ? NotFound() : Ok(platform.ToResponseModel());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePlatformRequest platform)
    {
        var addPlatform = platform.ToDomainModel();
        repository.AddPlatform(addPlatform);
        await repository.SaveChangesAsync();
        try
        {
            await commands.SendToCommandServiceAsync(addPlatform.ToResponseModel());
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Unable to send message to CommandsService");
            // ignored
        }

        return CreatedAtAction(nameof(Get), new {id = addPlatform.Id}, addPlatform.ToResponseModel());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdatePlatformRequest platform)
    {
        if (id != platform.Id)
            return BadRequest("Id's do not match");

        var platformToUpdate = await repository.GetPlatformById(id);
        if (platformToUpdate == null)
            return NotFound();

        platform.Update(platformToUpdate);

        await repository.SaveChangesAsync();
        try
        {
            await commands.SendToCommandServiceAsync(platformToUpdate.ToResponseModel());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to send message to CommandsService");
            // ignored
        }

        return Ok(platformToUpdate.ToResponseModel());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await repository.DeletePlatformAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}