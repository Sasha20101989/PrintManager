using Microsoft.AspNetCore.Mvc;
using PrintManager.Application.Contracts.Branch;
using PrintManager.Application.DTOs;
using PrintManager.Application.Interfaces;
using PrintManager.Logic.Models;
using System.Net;

namespace PrintManager.API.Controllers;

/// <summary>
/// Контроллер для управления филиалами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BranchController : ControllerBase
{
    /// <summary>
    /// Получает список филиалов с возможностью пагинации.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<Branch>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index(
        [FromServices] IBranchService branchService,
        [FromQuery] GetBranchRequest request)
    {
        IReadOnlyList<Branch> branches = await branchService.GetByPageAsync(request.Page, request.PageSize);

        return Ok(branches.Select(BranchDTO.Create));
    }
}
