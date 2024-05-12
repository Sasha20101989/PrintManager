using PrintManager.Application.Attributes;
using PrintManager.Application.Properties;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Application.Contracts.Installation
{
    public record CreateInstallationRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ContractsResources), ErrorMessageResourceName = "RequiredInstallationNameErrorMessage")]
        public required string InstallationName { get; init; }

        [Required(ErrorMessageResourceType = typeof(ContractsResources), ErrorMessageResourceName = "RequiredBranchIdErrorMessage")]
        [MinValue(1)]
        public required int BranchId { get; init; }

        [Required(ErrorMessageResourceType = typeof(ContractsResources), ErrorMessageResourceName = "RequiredPrinterIdErrorMessage")]
        [MinValue(1)]
        public required int PrinterId { get; init; }

        [Required(ErrorMessageResourceType = typeof(ContractsResources), ErrorMessageResourceName = "RequiredDefaultInstallationErrorMessage")]
        public required bool DefaultInstallation { get; init; }

        [NonNegative]
        public int? PrinterOrder { get; init; }
    }
}
