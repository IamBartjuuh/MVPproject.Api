using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectNaam.WebApi.Repository;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectNaam.WebApi.Controllers;

[ApiController]
[Route("Object2Ds")]
[Authorize]
public class Object2DController : ControllerBase
{
    private readonly Object2DRepository _Object2DRepository;
    private readonly ILogger<Object2DController> _logger;

    public Object2DController(Object2DRepository Object2DRepository, ILogger<Object2DController> logger)
    {
        _Object2DRepository = Object2DRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadObject2Ds")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Object2D>>> Get()
    {
        var Object2Ds = await _Object2DRepository.ReadAsync();
        return Ok(Object2Ds);
    }

    [HttpGet("{Object2DId}", Name = "ReadObject2D")]
    [Authorize]
    public async Task<ActionResult<Object2D>> Get(Guid Object2DId)
    {
        var Object2D = await _Object2DRepository.ReadAsync(Object2DId);
        if (Object2D == null)
            return NotFound();

        return Ok(Object2D);
    }

    [HttpPost(Name = "CreateObject2D")]
    [Authorize]
    public async Task<ActionResult> Add(Object2D Object2D)
    {
        Object2D.Id = Guid.NewGuid();

        var createdObject2D = await _Object2DRepository.InsertAsync(Object2D);
        return Created();
    }

    [HttpPut("{Object2DId}", Name = "UpdateObject2D")]
    [Authorize]
    public async Task<ActionResult> Update(Guid Object2DId, Object2D newObject2D)
    {
        var existingObject2D = await _Object2DRepository.ReadAsync(Object2DId);

        if (existingObject2D == null)
            return NotFound();

        await _Object2DRepository.UpdateAsync(newObject2D);

        return Ok(newObject2D);
    }

    [HttpDelete("{Object2DId}", Name = "DeleteObject2DByDate")]
    [Authorize]
    public async Task<IActionResult> Update(Guid Object2DId)
    {
        var existingObject2D = await _Object2DRepository.ReadAsync(Object2DId);

        if (existingObject2D == null)
            return NotFound();

        await _Object2DRepository.DeleteAsync(Object2DId);

        return Ok();
    }

}
