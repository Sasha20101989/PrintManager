using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Interfaces;

public interface IPrinterService
{
    Task<IReadOnlyList<Printer>> GetAllAsync(Logic.Enums.ConnectionType connectionType);
}
