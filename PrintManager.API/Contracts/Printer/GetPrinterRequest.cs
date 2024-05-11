using PrintManager.Applpication.DefaultValues;
using PrintManager.Logic.Enums;
using System.ComponentModel;

namespace PrintManager.API.Contracts.Printer;

/// <summary>
/// Запрос на получение списка принтеров.
/// </summary>
public record GetPrinterRequest
{
    /// <summary>
    /// Тип подключения
    /// </summary>
    public ConnectionType? ConnectionType { get; init; }

    /// <summary>
    /// Номер страницы
    /// </summary>
    [DefaultValue(DefaultPaginationValues.PrinterDefaultPage)]
    public int? Page { get; init; }

    /// <summary>
    /// Размер страницы (количество элементов на странице)
    /// </summary>
    [DefaultValue(DefaultPaginationValues.PrinterDefaultPageSize)]
    public int? PageSize { get; init; }
}
