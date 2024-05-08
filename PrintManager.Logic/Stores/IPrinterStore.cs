using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IPrinterStore
{
    Task<IReadOnlyList<Printer>> GetAllAsync(Enums.ConnectionType connectionType);
}
