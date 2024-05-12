using PrintManager.Logic.Models;

namespace PrintManager.Application.Interfaces;

public interface IPrinterService
{
    Printer? GetById(int printerId);

    Task<Printer?> GetByIdAsync(int printerId);

    Task<IReadOnlyList<Printer>> GetByPageAsync(int? page, int? pageSize, Logic.Enums.ConnectionType? connectionType);
}
