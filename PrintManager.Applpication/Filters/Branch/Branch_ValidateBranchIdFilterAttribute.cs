using Microsoft.AspNetCore.Mvc.Filters;
using PrintManager.Applpication.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PrintManager.Applpication.Contracts.Installation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PrintManager.Applpication.Filters.Branch
{
    public class Branch_ValidateBranchIdFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IBranchService branchService = context.HttpContext.RequestServices.GetRequiredService<IBranchService>();

            int? branchId = null;

            if (context.ActionArguments.TryGetValue("id", out object? value))
            {
                branchId = value as int?;
            }

            if (!branchId.HasValue && context.ActionArguments.TryGetValue("createInstallationRequest", out object? request))
            {
                CreateInstallationRequest? createInstallationRequest = request as CreateInstallationRequest;

                if (createInstallationRequest != null)
                {
                    branchId = createInstallationRequest.BranchId;
                }
            }

            if (!branchId.HasValue)
            {
                context.ModelState.AddModelError(nameof(Logic.Models.Branch.BranchId), $"{nameof(Logic.Models.Branch.BranchId)} not provided.");

                ValidationProblemDetails problemDetails = new(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };

                context.Result = new BadRequestObjectResult(problemDetails);

                return;
            }

            if (branchService.GetById(branchId.Value) is null)
            {
                context.ModelState.AddModelError(nameof(Logic.Models.Branch.BranchId), $"{nameof(Logic.Models.Branch)} with id {branchId} not found.");

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
