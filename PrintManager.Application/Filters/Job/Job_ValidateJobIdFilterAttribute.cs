using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PrintManager.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using PrintManager.Application.Properties;
using PrintManager.Logic.Models;

namespace PrintManager.Application.Filters.Job
{
    public class Job_ValidateJobIdFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IJobService jobService = context.HttpContext.RequestServices.GetRequiredService<IJobService>();

            int? jobId = context.ActionArguments["id"] as int?;

            if (jobId.HasValue)
            {
                if (jobId <= 0)
                {
                    context.ModelState.AddModelError(nameof(Logic.Models.Job.JobId), $"{string.Format(AttributesResources.ErrorMinValueValidationMessage, nameof(Logic.Models.Job.JobId), 1)}");

                    ValidationProblemDetails problemDetails = new(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };

                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else if (jobId is not null && jobService.GetById(jobId.Value) is null)
                {
                    context.ModelState.AddModelError(nameof(Logic.Models.Job.JobId), $"{string.Format(FiltersResources.ModelNotFound, nameof(Logic.Models.Job), jobId.Value)}");

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
