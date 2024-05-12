using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PrintManager.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

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
                    context.ModelState.AddModelError(nameof(Logic.Models.Job.JobId), $"{nameof(Logic.Models.Job.JobId)} must be greater than or equal to 1.");

                    ValidationProblemDetails problemDetails = new(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };

                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else if (jobId is not null && jobService.GetById(jobId.Value) is null)
                {
                    context.ModelState.AddModelError(nameof(Logic.Models.Job.JobId), $"{nameof(Logic.Models.Job)} with id {jobId.Value} not found.");

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
