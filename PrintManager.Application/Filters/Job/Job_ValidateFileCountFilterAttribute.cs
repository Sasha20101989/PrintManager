using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PrintManager.Application.Contracts.Job;
using Microsoft.Extensions.DependencyInjection;
using PrintManager.Application.Interfaces;

namespace PrintManager.Application.Filters.Job
{
    public class Job_ValidateFileCountFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IJobService jobService = context.HttpContext.RequestServices.GetRequiredService<IJobService>();

            IFormFileCollection? file = context.ActionArguments["file"] as IFormFileCollection;

            if (file is not null)
            {
                if (file.Count == 0)
                {
                    context.ModelState.AddModelError("File", $"File is required.");

                    ValidationProblemDetails problemDetails = new(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };

                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else
                {
                    try
                    {
                        IReadOnlyList<CsvJobData> csvData = jobService.ImportJobsFromCsv(file[0].OpenReadStream());
                    }
                    catch (Exception ex)
                    {
                        context.ModelState.AddModelError("File", ex.Message);

                        ValidationProblemDetails problemDetails = new(context.ModelState)
                        {
                            Status = StatusCodes.Status422UnprocessableEntity,
                        };

                        context.Result = new UnprocessableEntityObjectResult(problemDetails);
                    }
                }
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
