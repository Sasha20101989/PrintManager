using Microsoft.AspNetCore.Mvc;
using PrintManager.API.Contracts.Printer;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Enums;
using PrintManager.Logic.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PrintManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrinterController : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Получить список принтеров с указанным типом подключения")]
        [SwaggerResponse(StatusCodes.Status200OK, "Список принтеров", typeof(IReadOnlyList<Printer>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Указанный тип подключения не поддерживается")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Index(
            [FromServices] IPrinterService printerService,
            [FromQuery] GetPrinterRequest request)
        {
            if (!Enum.IsDefined(typeof(Logic.Enums.ConnectionType), request.ConnectionType))
            {
                return BadRequest("The specified connection type is not supported.");
            }

            IReadOnlyList<Printer> printers = await printerService.GetAllAsync(request.ConnectionType);

            return Ok(printers);
        }
    }
}
