using PrintManager.Applpication.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Applpication.Contracts.Job
{
    public record CreateJobRequest
    {
        [Required(ErrorMessage = "PrintJobName is required.")]
        public required string PrintJobName { get; init; }

        [Required]
        [MinValue(1)]
        public required int EmployeeId { get; init; }

        [NonNegative]
        public int? PrinterId { get; init; }

        [Required]
        [MinValue(1)]
        public required int PagesPrinted { get; init; }
    }
}
