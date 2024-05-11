using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PrintManager.API.Contracts.Installation;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace PrintManager.API.Controllers;

/// <summary>
/// Контроллер для управления инсталяциями.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class InstallationController : ControllerBase
{
    /// <summary>
    /// Создание инсталяции
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Installation), (int)HttpStatusCode.Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(
        [FromServices] IInstallationService installationService,
        [FromServices] IBranchService branchService,
        [FromServices] IPrinterService printerService,
        [FromBody] CreateInstallationRequest request)
    {
        Branch? existingBranch = await branchService.GetByIdAsync(request.BranchId);

        if (existingBranch is null)
        {
            return NotFound($"Branch with id {request.BranchId} not found.");
        }

        Printer? existingPrinter = await printerService.GetByIdAsync(request.PrinterId);

        if (existingPrinter is null)
        {
            return NotFound($"Printer with id {request.PrinterId} not found.");
        }

        Installation newInstallation = await installationService.CreateAsync(request.InstallationName, existingBranch, existingPrinter, request.DefaultInstallation, request.PrinterOrder == 0 ? null : request.PrinterOrder);

        return CreatedAtAction(nameof(GetById), new { id = newInstallation.InstallationId }, newInstallation);
    }

    /// <summary>
    /// Получение сведений об инсталляции по параметру, позволяющему её уникально идентифицировать
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Installation), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
    [FromServices] IInstallationService installationService,
    int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than or equal to 1.");
        }

        Installation? installation = await installationService.GetByIdAsync(id);

        if (installation is null)
        {
            return NotFound($"Installation with id {id} not found.");
        }

        return Ok(installation);
    }

    /// <summary>
    /// Получение списка всех имеющихся инсталляций с возможностью фильтрации по конкретному филиалу
    /// </summary>
    [HttpGet("branch")]
    [ProducesResponseType(typeof(IReadOnlyList<Installation>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> IndexByBranchName(
    [FromServices] IInstallationService installationService,
    [FromQuery] GetInstallationRequest request)
    {
        IReadOnlyList<Installation> installations = await installationService.GetByBranchNameAsync(request.Branch, request.Page, request.PageSize);

        return Ok(installations);
    }

    /// <summary>
    /// Удаление записи об инсталляции устройства печати
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(IReadOnlyList<Installation>), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(
        [FromServices] IInstallationService installationService,
        int id)
    {
        Installation? installation = await installationService.GetByIdAsync(id);

        if (installation is null)
        {
            return NotFound($"Installation with id {id} not found.");
        }

        await installationService.DeleteAsync(installation);

        return NoContent();
    }
}
