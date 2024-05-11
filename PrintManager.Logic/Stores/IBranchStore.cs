using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IBranchStore
{
    Task<IReadOnlyList<Branch>> GetAllAsync();

    Branch? GetById(int branchId);

    Task<Branch?> GetByIdAsync(int branchId);

    Task<IReadOnlyList<Branch>> GetByPageAsync(int skip, int pageSize);
}
