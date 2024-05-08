using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Applpication.Services;

public class PrinterService(IPrinterStore printerStore) : IPrinterService
{
    public async Task<IReadOnlyList<Printer>> GetAllAsync(Logic.Enums.ConnectionType connectionType)
    {
        return await printerStore.GetAllAsync(connectionType);
    }
}
