using PrintManager.Application.Attributes;
using PrintManager.Application.Properties;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Application.Contracts.Job
{
    public record CreateJobRequest
    {
        [Required(ErrorMessageResourceType = typeof(ContractsResources), ErrorMessageResourceName = "RequiredPrintJobNameErrorMessage")]
        public required string PrintJobName { get; init; }

        [Required(ErrorMessageResourceType = typeof(ContractsResources), ErrorMessageResourceName = "RequiredEmployeeIdErrorMessage")]
        [MinValue(1)]
        public required int EmployeeId { get; init; }

        [NonNegative]
        public int? PrinterId { get; init; }

        [Required(ErrorMessageResourceType = typeof(ContractsResources), ErrorMessageResourceName = "RequiredPagesPrintedErrorMessage")]
        [MinValue(1)]
        public required int PagesPrinted { get; init; }
    }
}
