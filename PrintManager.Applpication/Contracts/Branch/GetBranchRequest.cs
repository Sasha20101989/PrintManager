using PrintManager.Applpication.DefaultValues;
using System.ComponentModel;

namespace PrintManager.Applpication.Contracts.Branch;

/// <summary>
/// Запрос на получение списка филиалов с возможностью пагинации.
/// </summary>
public record class GetBranchRequest
{
    /// <summary>
    /// Номер страницы
    /// </summary>
    [DefaultValue(DefaultPaginationValues.BranchDefaultPage)]
    public int? Page { get; init; }

    /// <summary>
    /// Размер страницы (количество элементов на странице).
    /// </summary>
    [DefaultValue(DefaultPaginationValues.BranchDefaultPageSize)]
    public int? PageSize { get; init; }
}
