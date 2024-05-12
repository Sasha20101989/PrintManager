using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IPrinterStore
{
    Task<IReadOnlyList<Printer>> GetAllAsync(string? connectionType = null);

    Task<Printer?> GetByIdAsync(int printerId);

    Printer? GetById(int printerId);

    Task<IReadOnlyList<Printer>> GetByPageAsync(int skip, int pageSize, Enums.ConnectionTypes? connectionType);
}
