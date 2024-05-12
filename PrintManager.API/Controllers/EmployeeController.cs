using Microsoft.AspNetCore.Mvc;
using PrintManager.Application.Contracts.Employee;
using PrintManager.Application.Interfaces;
using PrintManager.Application.RDOs;
using PrintManager.Logic.Models;
using System.Collections.Generic;
using System.Net;

namespace PrintManager.API.Controllers;

/// <summary>
/// Контроллер для управления работниками.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    /// <summary>
    /// Получение списка работников с возможностью пагинации.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<Employee>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index(
        [FromServices] IEmployeeService employeeService,
        [FromQuery] GetEmployeeRequest request)
    {
        IReadOnlyList<Employee> empoyees = await employeeService.GetByPageAsync(request.Page, request.PageSize);

        return Ok(empoyees.Select(e => EmployeeRDO.Create(e.EmployeeId, e.EmployeeName, e.Branch)));
    }
}
