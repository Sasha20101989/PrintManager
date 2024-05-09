using PrintManager.Applpication.DefaultValues;
using System.ComponentModel;

namespace PrintManager.API.Contracts.Employee;

/// <summary>
/// Запрос на получение списка работников с возможностью пагинации.
/// </summary>
public record class GetEmployeeRequest
{
    /// <summary>
    /// Номер страницы
    /// </summary>
    [DefaultValue(DefaultPaginationValues.EmployeeDefaultPage)]
    public int Page { get; init; }

    /// <summary>
    /// Размер страницы (количество элементов на странице).
    /// </summary>
    [DefaultValue(DefaultPaginationValues.EmployeeDefaultPageSize)]
    public int PageSize { get; init; }
}
