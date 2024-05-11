using Microsoft.AspNetCore.Mvc;
using PrintManager.API.Contracts.Employee;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace PrintManager.API.Controllers
{
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
        /// <param name="employeeService">Сервис для работы с работниками.</param>
        /// <param name="request">Запрос на получение списка работников с пагинацией.</param>
        /// <returns>Результат выполнения запроса с данными о работниках.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<Employee>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Index(
            [FromServices] IEmployeeService employeeService,
            [FromQuery] GetEmployeeRequest request)
        {
            IReadOnlyList<Employee> employees = await employeeService.GetByPageAsync(request.Page, request.PageSize);

            return Ok(employees);
        }
    }
}
