using PrintManager.Application.DefaultValues;
using PrintManager.Application.Properties;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Application.Contracts.Installation
{
    public record GetInstallationRequest
    {
        /// <summary>
        /// Номер страницы
        /// </summary>
        [DefaultValue(DefaultPaginationValues.InstallationDefaultPage)]
        public int? Page { get; init; }

        /// <summary>
        /// Размер страницы (количество элементов на странице).
        /// </summary>
        [DefaultValue(DefaultPaginationValues.InstallationDefaultPageSize)]
        public int? PageSize { get; init; }

        /// <summary>
        /// Название филиала.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ContractsResources), ErrorMessageResourceName = "RequiredBranchNameErrorMessage")]
        public required string Branch { get; init; } = null!;
    }
}
