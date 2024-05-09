using PrintManager.Applpication.DefaultValues;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Applpication.Services;

public class PrinterService(IPrinterStore printerStore) : IPrinterService
{
    public async Task<IReadOnlyList<Printer>> GetByPageAsync(int ? page, int? pageSize, string? connectionType = null)
    {
        int pageNumber = page.HasValue && page > 0 ? page.Value : DefaultPaginationValues.PrinterDefaultPage;
        int size = pageSize.HasValue && pageSize > 0 ? pageSize.Value : DefaultPaginationValues.PrinterDefaultPageSize;

        int skip = (pageNumber - 1) * size;

        return await printerStore.GetByPageAsync(skip, size, connectionType);
    }
}
