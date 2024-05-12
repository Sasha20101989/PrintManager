using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PrintManager.Application.Contracts.Installation;
using PrintManager.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using PrintManager.Application.Contracts.Job;

namespace PrintManager.Application.Filters.Printer
{
    public class Printer_ValidatePrinterIdFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IPrinterService printerService = context.HttpContext.RequestServices.GetRequiredService<IPrinterService>();

            int? printerId = null;

            if (context.ActionArguments.TryGetValue("id", out object? value))
            {
                printerId = value as int?;
            }

            if (!printerId.HasValue && context.ActionArguments.TryGetValue("createInstallationRequest", out object? installationRequest))
            {
                CreateInstallationRequest? createInstallationRequest = installationRequest as CreateInstallationRequest;

                if (createInstallationRequest != null)
                {
                    printerId = createInstallationRequest.PrinterId;
                }
            }else if (!printerId.HasValue && context.ActionArguments.TryGetValue("createJobRequest", out object? jobRequest))
            {
                CreateJobRequest? createJobRequest = jobRequest as CreateJobRequest;

                if (createJobRequest != null)
                {
                    printerId = createJobRequest.PrinterId;
                }
            }

            if (!printerId.HasValue)
            {
                context.ModelState.AddModelError(nameof(Logic.Models.Printer.PrinterId), $"{nameof(Logic.Models.Printer.PrinterId)} not provided.");

                ValidationProblemDetails problemDetails = new(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };

                context.Result = new BadRequestObjectResult(problemDetails);

                return;
            }

            if (printerService.GetById(printerId.Value) is null)
            {
                context.ModelState.AddModelError(nameof(Logic.Models.Printer.PrinterId), $"{nameof(Logic.Models.Printer)} with id {printerId} not found.");

                ValidationProblemDetails problemDetails = new(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound,
                };

                context.Result = new BadRequestObjectResult(problemDetails);

                return;
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
