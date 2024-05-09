
using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Interfaces;

public interface IBranchService
{
    Task<IReadOnlyList<Branch>> GetByPageAsync(int? page, int? pageSize);
}
