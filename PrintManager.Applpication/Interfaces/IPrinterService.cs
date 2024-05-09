using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Interfaces;

public interface IPrinterService
{
    Task<IReadOnlyList<Printer>> GetByPageAsync(int? page, int? pageSize, string? connectionType = null);
}
