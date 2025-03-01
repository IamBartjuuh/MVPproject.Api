using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectNaam.WebApi.Repository;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectNaam.WebApi.Controllers;

[ApiController]
[Route("Environments")]
[Authorize]
public class Environment2DController : ControllerBase
{
    private readonly Environment2DRepository _Environment2DRepository;
    private readonly ILogger<Environment2DController> _logger;

    public Environment2DController(Environment2DRepository Environment2DRepository, ILogger<Environment2DController> logger)
    {
        _Environment2DRepository = Environment2DRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadEnvironment2Ds")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Environment2D>>> Get()
    {
        var Environment2Ds = await _Environment2DRepository.ReadAsync();
        return Ok(Environment2Ds);
    }

    [HttpGet("{Environment2DId}", Name = "ReadEnvironment2D")]
    [Authorize]
    public async Task<ActionResult<Environment2D>> Get(Guid Environment2DId)
    {
        var Environment2D = await _Environment2DRepository.ReadAsync(Environment2DId);
        if (Environment2D == null)
            return NotFound();

        return Ok(Environment2D);
    }

    [HttpPost(Name = "CreateEnvironment2D")]
    [Authorize]
    public async Task<ActionResult> Add(Environment2D Environment2D)
    {
        Environment2D.Id = Guid.NewGuid();

        var createdEnvironment2D = await _Environment2DRepository.InsertAsync(Environment2D);
        return Created();
    }

    [HttpPut("{Environment2DId}", Name = "UpdateEnvironment2D")]
    [Authorize]
    public async Task<ActionResult> Update(Guid Environment2DId, Environment2D newEnvironment2D)
    {
        var existingEnvironment2D = await _Environment2DRepository.ReadAsync(Environment2DId);

        if (existingEnvironment2D == null)
            return NotFound();

        await _Environment2DRepository.UpdateAsync(newEnvironment2D);

        return Ok(newEnvironment2D);
    }

    [HttpDelete("{Environment2DId}", Name = "DeleteEnvironment2DByDate")]
    [Authorize]
    public async Task<IActionResult> Update(Guid Environment2DId)
    {
        var existingEnvironment2D = await _Environment2DRepository.ReadAsync(Environment2DId);

        if (existingEnvironment2D == null)
            return NotFound();

        await _Environment2DRepository.DeleteAsync(Environment2DId);

        return Ok();
    }

}
