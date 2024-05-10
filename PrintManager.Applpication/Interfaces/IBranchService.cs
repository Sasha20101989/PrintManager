
using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Interfaces;

public interface IBranchService
{
    Task<Branch?> GetByIdAsync(int branchId);

    Task<IReadOnlyList<Branch>> GetByPageAsync(int? page, int? pageSize);
}
