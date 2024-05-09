using Microsoft.AspNetCore.Mvc;
using PrintManager.API.Contracts.Branch;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;

namespace PrintManager.API.Controllers
{
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
        /// <param name="branchService">Сервис для работы с филиалами.</param>
        /// <param name="request">Запрос на получение филиалов.</param>
        /// <returns>Список филиалов.</returns>
        [HttpGet]
        public async Task<IActionResult> Index(
            [FromServices] IBranchService branchService,
            [FromQuery] GetBranchRequest request)
        {
            IReadOnlyList<Branch> branches = await branchService.GetByPageAsync(request.Page, request.PageSize);

            return Ok(branches);
        }
    }
}
