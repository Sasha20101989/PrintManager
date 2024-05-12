using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IStatusStore
{
    Task<Status?> GetByIdAsync(int id);
}
