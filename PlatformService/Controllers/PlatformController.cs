using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Interfaces;
using PlatformService.Models;
using PlatformService.Models.Mappers;
using PlatformService.Request;

namespace PlatformService.Controllers;

[ApiController, Route("api/platforms")]
public class PlatformController : ControllerBase
{
    private readonly IPlatformRepo _repository;

    public PlatformController(IPlatformRepo repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var platforms = await _repository.GetAllPlatformsAsync();
        return Ok(platforms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var platform = await _repository.GetPlatformById(id);
        return platform == null ? NotFound() : Ok(platform.ToResponseModel());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePlatformRequest platform)
    {
        var addPlatform = platform.ToDomainModel();
        _repository.AddPlatform(addPlatform);
        await _repository.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = addPlatform.Id }, addPlatform.ToResponseModel());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdatePlatformRequest platform)
    {
        if (id != platform.Id) 
            return BadRequest("Id's do not match");
        
        var platformToUpdate = await _repository.GetPlatformById(id);
        if (platformToUpdate == null) 
            return NotFound();
        
        platform.Update(platformToUpdate);
        
        await _repository.SaveChangesAsync();
        return Ok(platformToUpdate.ToResponseModel());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted = await _repository.DeletePlatformAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}