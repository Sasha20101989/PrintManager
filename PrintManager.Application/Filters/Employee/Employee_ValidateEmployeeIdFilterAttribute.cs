using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PrintManager.Application.Contracts.Job;
using PrintManager.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using PrintManager.Application.Properties;
using PrintManager.Logic.Models;

namespace PrintManager.Application.Filters.Employee
{
    public class Employee_ValidateEmployeeIdFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IEmployeeService employeeService = context.HttpContext.RequestServices.GetRequiredService<IEmployeeService>();

            int? employeeId = null;

            if (context.ActionArguments.TryGetValue("id", out object? value))
            {
                employeeId = value as int?;
            }

            if (!employeeId.HasValue && context.ActionArguments.TryGetValue("createJobRequest", out object? jobRequest))
            {
                CreateJobRequest? createJobRequest = jobRequest as CreateJobRequest;

                if (createJobRequest != null)
                {
                    employeeId = createJobRequest.EmployeeId;
                }
            }

            if (!employeeId.HasValue)
            {
                context.ModelState.AddModelError(nameof(Logic.Models.Employee.EmployeeId), $"{string.Format(FiltersResources.FilterNotProvided, nameof(Logic.Models.Employee.EmployeeId))}");

                ValidationProblemDetails problemDetails = new(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };

                context.Result = new BadRequestObjectResult(problemDetails);

                return;
            }

            if (employeeService.GetById(employeeId.Value) is null)
            {
                context.ModelState.AddModelError(nameof(Logic.Models.Employee.EmployeeId), $"{string.Format(FiltersResources.ModelNotFound, nameof(Logic.Models.Employee), employeeId)}");

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
