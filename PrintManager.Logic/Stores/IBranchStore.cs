using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IBranchStore
{
    Task<IReadOnlyList<Branch>> GetAllAsync();
}
