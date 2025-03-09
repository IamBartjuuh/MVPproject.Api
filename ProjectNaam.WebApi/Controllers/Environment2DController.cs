using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectNaam.WebApi.Repository;
using ProjectNaam.WebApi.Services;
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
    private readonly IAuthenticationService _authenticationService;

    public Environment2DController(Environment2DRepository Environment2DRepository, ILogger<Environment2DController> logger, IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        _Environment2DRepository = Environment2DRepository;
        _logger = logger;
    }

    [HttpGet(Name = "ReadEnvironment2Ds")]
    public async Task<ActionResult<IEnumerable<Environment2D>>> Get()
    {
        Guid CurrentUser = Guid.Parse(_authenticationService.GetCurrentAuthenticatedUserId());
        var Environment2Ds = await _Environment2DRepository.ReadForUserAsync(CurrentUser);
        return Ok(Environment2Ds);
    }

    [HttpGet("{Environment2DId}", Name = "ReadEnvironment2D")]
    public async Task<ActionResult<Environment2D>> Get(Guid Environment2DId)
    {
        var Environment2D = await _Environment2DRepository.ReadAsync(Environment2DId);
        if (Environment2D == null)
            return NotFound();

        return Ok(Environment2D);
    }

    [HttpPost(Name = "CreateEnvironment2D")]
    public async Task<ActionResult> Add(Environment2D Environment2D)
    {
        Guid CurrentUser = Guid.Parse(_authenticationService.GetCurrentAuthenticatedUserId());
        var Environment2Ds = await _Environment2DRepository.ReadForUserAsync(CurrentUser);
        if(Environment2Ds.Count() >= 5)
        {
            return BadRequest();
        } 
        else
        {
            Environment2D.Id = Guid.NewGuid();

            Environment2D.OwnerUserId = _authenticationService.GetCurrentAuthenticatedUserId();

            var createdEnvironment2D = await _Environment2DRepository.InsertAsync(Environment2D);
            return Ok();
        }

    }

    [HttpPut("{Environment2DId}", Name = "UpdateEnvironment2D")]
    public async Task<ActionResult> Update(Guid Environment2DId, Environment2D newEnvironment2D)
    {
        var existingEnvironment2D = await _Environment2DRepository.ReadAsync(Environment2DId);

        if (existingEnvironment2D == null)
            return NotFound();

        await _Environment2DRepository.UpdateAsync(newEnvironment2D);

        return Ok(newEnvironment2D);
    }

    [HttpDelete("{Environment2DId}", Name = "DeleteEnvironment2DByDate")]
    public async Task<IActionResult> Update(Guid Environment2DId)
    {
        var existingEnvironment2D = await _Environment2DRepository.ReadAsync(Environment2DId);

        if (existingEnvironment2D == null)
            return NotFound();

        await _Environment2DRepository.DeleteAsync(Environment2DId);

        return Ok();
    }

}
