using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IPrinterStore
{
    Task<IReadOnlyList<Printer>> GetAllAsync(string? connectionType = null);
    Task<IReadOnlyList<Printer>> GetByPageAsync(int skip, int pageSize, string? connectionType = null);
}
