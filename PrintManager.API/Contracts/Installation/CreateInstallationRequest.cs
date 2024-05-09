using System.ComponentModel.DataAnnotations;

namespace PrintManager.API.Contracts.Installation
{
    public record CreateInstallationRequest
    {
        [Required]
        public required string InstallationName { get; init; }

        [Required]
        public required int BranchId { get; init; }

        [Required]
        public required int PrinterId { get; init; }

        public int? PrinterOrder { get; init; }

        [Required]
        public required bool DefaultInstallation { get; init; }
    }
}
