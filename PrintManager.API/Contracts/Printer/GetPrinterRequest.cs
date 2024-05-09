using PrintManager.Applpication.DefaultValues;
using System.ComponentModel;

namespace PrintManager.API.Contracts.Printer;

/// <summary>
/// Запрос на получение списка принтеров.
/// </summary>
public record GetPrinterRequest
{
    /// <summary>
    /// Тип подключения (local для локального, network для сетевого).
    /// </summary>
    public string? ConnectionType { get; init; }

    /// <summary>
    /// Номер страницы
    /// </summary>
    [DefaultValue(DefaultPaginationValues.PrinterDefaultPage)]
    public int? Page { get; init; }

    /// <summary>
    /// Размер страницы (количество элементов на странице).
    /// </summary>
    [DefaultValue(DefaultPaginationValues.PrinterDefaultPageSize)]
    public int? PageSize { get; init; }
}
