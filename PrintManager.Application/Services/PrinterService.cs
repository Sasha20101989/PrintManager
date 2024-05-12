using PrintManager.Application.DefaultValues;
using PrintManager.Application.Interfaces;
using PrintManager.Application.Parameters;
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

    public async Task<IReadOnlyList<Printer>> GetByPageAsync(int ? page, int? pageSize, Logic.Enums.ConnectionTypes? connectionType)
    {
        PaginationParameters paginationParameters = PaginationParameters.Create(page, pageSize, DefaultPaginationValues.PrinterDefaultPage, DefaultPaginationValues.PrinterDefaultPageSize);

        return await printerStore.GetByPageAsync(paginationParameters.Skip, paginationParameters.Size, connectionType);
    }
}
