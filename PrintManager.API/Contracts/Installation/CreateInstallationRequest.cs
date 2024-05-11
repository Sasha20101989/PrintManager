using PrintManager.Applpication.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.API.Contracts.Installation
{
    public record CreateInstallationRequest
    {
        [Required(ErrorMessage = "Installation name is required.")]
        public required string InstallationName { get; init; }

        [Required]
        [MinValue(1)]
        public required int BranchId { get; init; }

        [Required]
        [MinValue(1)]
        public required int PrinterId { get; init; }

        [Required]
        public required bool DefaultInstallation { get; init; }

        [NonNegative]
        public int? PrinterOrder { get; init; }
    }
}
