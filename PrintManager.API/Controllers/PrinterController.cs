using Microsoft.AspNetCore.Mvc;
using PrintManager.API.Contracts.Printer;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using Swashbuckle.AspNetCore.Annotations;

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
        /// Получение принтеров с указанным типом подключения
        /// </summary>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Index(
            [FromServices] IPrinterService printerService,
            [FromQuery] GetPrinterRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.ConnectionType) &&
                !Enum.TryParse<Logic.Enums.ConnectionType>(request.ConnectionType, true, out _))
            {
                return BadRequest("The specified connection type is not supported.");
            }

            IReadOnlyList<Printer> printers = await printerService.GetByPageAsync(request.Page, request.PageSize, request.ConnectionType);

            return Ok(printers);
        }
    }
}
