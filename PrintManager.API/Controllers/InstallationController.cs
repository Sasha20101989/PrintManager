using Microsoft.AspNetCore.Mvc;
using PrintManager.API.Contracts.Installation;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PrintManager.API.Controllers;

/// <summary>
/// Контроллер для управления инсталяциями.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InstallationController : ControllerBase
{
    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] IInstallationService installationService, 
        int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        Installation? installation = await installationService.GetByIdAsync(id);

        if (installation is null)
        {
            return NotFound();
        }

        return Ok(installation);
    }

    [HttpGet("branch")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> IndexByBranchNameAsync(
        [FromServices] IInstallationService installationService,
        [FromQuery] GetInstallationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Branch))
        {
            return BadRequest();
        }

        IReadOnlyList<Installation> installations = await installationService.GetByBranchNameAsync(request.Branch, request.Page, request.PageSize);

        return Ok(installations);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync()
    {
        return Ok();
    }
}
