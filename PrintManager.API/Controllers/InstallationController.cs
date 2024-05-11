using Microsoft.AspNetCore.Mvc;
using PrintManager.Applpication.Contracts.Installation;
using PrintManager.Applpication.Filters.Branch;
using PrintManager.Applpication.Filters.Installation;
using PrintManager.Applpication.Filters.Printer;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
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
    [ServiceFilter(typeof(Branch_ValidateBranchIdFilterAttribute))]
    [ServiceFilter(typeof(Printer_ValidatePrinterIdFilterAttribute))]
    public async Task<IActionResult> Create(
        [FromServices] IInstallationService installationService,
        [FromBody] CreateInstallationRequest createInstallationRequest)
    {
        Installation newInstallation = await installationService.CreateAsync(
            createInstallationRequest.InstallationName,
            createInstallationRequest.BranchId,
            createInstallationRequest.PrinterId,
            createInstallationRequest.DefaultInstallation,
            createInstallationRequest.PrinterOrder == 0 ? null : createInstallationRequest.PrinterOrder);

        return CreatedAtAction(nameof(GetById), new { id = newInstallation.InstallationId }, newInstallation);
    }

    /// <summary>
    /// Получение сведений об инсталляции по параметру, позволяющему её уникально идентифицировать
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Installation), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ServiceFilter(typeof(Installation_ValidateInstallationIdFilterAttribute))]
    public async Task<IActionResult> GetById(
    [FromServices] IInstallationService installationService,
    int id)
    {
        return Ok(await installationService.GetByIdAsync(id));
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
        [FromQuery] GetInstallationRequest getInstallationRequest)
    {
        return Ok(await installationService.GetByBranchNameAsync(getInstallationRequest.Branch, getInstallationRequest.Page, getInstallationRequest.PageSize));
    }

    /// <summary>
    /// Удаление записи об инсталляции устройства печати
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(IReadOnlyList<Installation>), (int)HttpStatusCode.NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ServiceFilter(typeof(Installation_ValidateInstallationIdFilterAttribute))]
    public async Task<IActionResult> Delete(
        [FromServices] IInstallationService installationService,
        int id)
    {
        await installationService.DeleteAsync(await installationService.GetByIdAsync(id));

        return NoContent();
    }
}
