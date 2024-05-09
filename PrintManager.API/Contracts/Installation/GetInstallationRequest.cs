using PrintManager.Applpication.DefaultValues;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.API.Contracts.Installation
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
        [Required]
        public required string? Branch { get; init; }
    }
}
