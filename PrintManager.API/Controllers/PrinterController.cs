using Microsoft.AspNetCore.Mvc;
using PrintManager.API.Contracts.Printer;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using System.Net;

namespace PrintManager.API.Controllers
{
    /// <summary>
    /// Контроллер для управления принтерами.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PrinterController : ControllerBase
    {
        /// <summary>
        /// Получение принтеров с возможностью фильтрации по типу и пагинации
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<Printer>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Index(
            [FromServices] IPrinterService printerService,
            [FromQuery] GetPrinterRequest request)
        {
            IReadOnlyList<Printer> printers = await printerService.GetByPageAsync(request.Page, request.PageSize, request.ConnectionType);

            return Ok(printers);
        }
    }
}
