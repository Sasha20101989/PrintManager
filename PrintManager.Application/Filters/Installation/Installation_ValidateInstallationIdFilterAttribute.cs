using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PrintManager.Application.Interfaces;

namespace PrintManager.Application.Filters.Installation
{
    public class Installation_ValidateInstallationIdFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IInstallationService installationService = context.HttpContext.RequestServices.GetRequiredService<IInstallationService>();

            int? installationId = context.ActionArguments["id"] as int?;

            if (installationId.HasValue)
            {
                if (installationId <= 0)
                {
                    context.ModelState.AddModelError(nameof(Logic.Models.Installation.InstallationId), $"{nameof(Logic.Models.Installation.InstallationId)} must be greater than or equal to 1.");

                    ValidationProblemDetails problemDetails = new(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };

                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else if (installationId is not null && installationService.GetById(installationId.Value) is null)
                {
                    context.ModelState.AddModelError(nameof(Logic.Models.Installation.InstallationId), $"{nameof(Logic.Models.Installation)} with id {installationId.Value} not found.");

                    ValidationProblemDetails problemDetails = new(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound,
                    };

                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }         

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
