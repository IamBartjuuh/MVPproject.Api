using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectNaam.WebApi.Repository;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectNaam.WebApi.Controllers;

[ApiController]
[Route("Enviroment2Ds")]
[Authorize]
public class Enviroment2DController : ControllerBase
{
    private readonly Enviroment2DRepository _Enviroment2DRepository;
    private readonly ILogger<Enviroment2DController> _logger;

    public Enviroment2DController(Enviroment2DRepository Enviroment2DRepository, ILogger<Enviroment2DController> logger)
    {
        _Enviroment2DRepository = Enviroment2DRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadEnviroment2Ds")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Enviroment2D>>> Get()
    {
        var Enviroment2Ds = await _Enviroment2DRepository.ReadAsync();
        return Ok(Enviroment2Ds);
    }

    [HttpGet("{Enviroment2DId}", Name = "ReadEnviroment2D")]
    [Authorize]
    public async Task<ActionResult<Enviroment2D>> Get(Guid Enviroment2DId)
    {
        var Enviroment2D = await _Enviroment2DRepository.ReadAsync(Enviroment2DId);
        if (Enviroment2D == null)
            return NotFound();

        return Ok(Enviroment2D);
    }

    [HttpPost(Name = "CreateEnviroment2D")]
    [Authorize]
    public async Task<ActionResult> Add(Enviroment2D Enviroment2D)
    {
        Enviroment2D.Id = Guid.NewGuid();

        var createdEnviroment2D = await _Enviroment2DRepository.InsertAsync(Enviroment2D);
        return Created();
    }

    [HttpPut("{Enviroment2DId}", Name = "UpdateEnviroment2D")]
    [Authorize]
    public async Task<ActionResult> Update(Guid Enviroment2DId, Enviroment2D newEnviroment2D)
    {
        var existingEnviroment2D = await _Enviroment2DRepository.ReadAsync(Enviroment2DId);

        if (existingEnviroment2D == null)
            return NotFound();

        await _Enviroment2DRepository.UpdateAsync(newEnviroment2D);

        return Ok(newEnviroment2D);
    }

    [HttpDelete("{Enviroment2DId}", Name = "DeleteEnviroment2DByDate")]
    [Authorize]
    public async Task<IActionResult> Update(Guid Enviroment2DId)
    {
        var existingEnviroment2D = await _Enviroment2DRepository.ReadAsync(Enviroment2DId);

        if (existingEnviroment2D == null)
            return NotFound();

        await _Enviroment2DRepository.DeleteAsync(Enviroment2DId);

        return Ok();
    }

}
