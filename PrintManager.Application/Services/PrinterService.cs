using PrintManager.Application.DefaultValues;
using PrintManager.Application.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Application.Services;

public class PrinterService(IPrinterStore printerStore) : IPrinterService
{
    public Printer? GetById(int printerId)
    {
        return printerStore.GetById(printerId);
    }

    public async Task<Printer?> GetByIdAsync(int printerId)
    {
        return await printerStore.GetByIdAsync(printerId);
    }

    public async Task<IReadOnlyList<Printer>> GetByPageAsync(int ? page, int? pageSize, Logic.Enums.ConnectionType? connectionType)
    {
        int pageNumber = page.HasValue && page > 0 ? page.Value : DefaultPaginationValues.PrinterDefaultPage;
        int size = pageSize.HasValue && pageSize > 0 ? pageSize.Value : DefaultPaginationValues.PrinterDefaultPageSize;

        int skip = (pageNumber - 1) * size;

        return await printerStore.GetByPageAsync(skip, size, connectionType);
    }
}
