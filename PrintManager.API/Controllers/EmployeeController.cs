using Microsoft.AspNetCore.Mvc;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;

namespace PrintManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index([FromServices] IEmployeeService employeeService)
        {
            IReadOnlyList<Employee> employees = await employeeService.GetAllAsync();

            return Ok(employees);
        }
    }
}
