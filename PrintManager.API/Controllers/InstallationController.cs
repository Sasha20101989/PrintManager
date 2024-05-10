using Microsoft.AspNetCore.Mvc;
using PrintManager.API.Contracts.Installation;
using PrintManager.Applpication.Interfaces;
using PrintManager.Applpication.Services;
using PrintManager.Logic.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection;

namespace PrintManager.API.Controllers;

/// <summary>
/// Контроллер для управления инсталяциями.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InstallationController : ControllerBase
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(
        [FromServices] IInstallationService installationService,
        [FromServices] IBranchService branchService,
        [FromServices] IPrinterService printerService,
        [FromBody] CreateInstallationRequest request)
    {
        if (request.InstallationName is null)
        {
            return BadRequest("Installation name is required.");
        }

        if (request.PrinterId <= 0)
        {
            return BadRequest();
        }

        if (request.BranchId <= 0)
        {
            return BadRequest();
        }

        if (request.PrinterOrder < 0)
        {
            return BadRequest();
        } 

        Branch? existingBranch = await branchService.GetByIdAsync(request.BranchId);

        if (existingBranch is null)
        {
            return NotFound();
        }

        Printer? existingPrinter = await printerService.GetByIdAsync(request.PrinterId);

        if (existingPrinter is null)
        {
            return NotFound();
        }

        Installation newInstallation = await installationService.CreateAsync(request.InstallationName, existingBranch, existingPrinter, request.DefaultInstallation, request.PrinterOrder == 0 ? null : request.PrinterOrder);

        return CreatedAtAction(nameof(GetById), new { id = newInstallation.InstallationId }, newInstallation);
    }

    [HttpGet]
    [Route("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
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
    public async Task<IActionResult> IndexByBranchName(
        [FromServices] IInstallationService installationService,
        [FromQuery] GetInstallationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Branch))
        {
            return BadRequest("Branch name is required.");
        }

        IReadOnlyList<Installation> installations = await installationService.GetByBranchNameAsync(request.Branch, request.Page, request.PageSize);

        return Ok(installations);
    }

    [HttpDelete("{installationId}")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromServices] IInstallationService installationService, 
        int installationId)
    {
        Installation? installation = await installationService.GetByIdAsync(installationId);

        if (installation is null)
        {
            return NotFound();
        }

        await installationService.DeleteAsync(installationId);

        return NoContent();
    }
}
