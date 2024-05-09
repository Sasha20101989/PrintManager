using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IBranchStore
{
    Task<IReadOnlyList<Branch>> GetAllAsync();

    Task<IReadOnlyList<Branch>> GetByPageAsync(int skip, int pageSize);
}
