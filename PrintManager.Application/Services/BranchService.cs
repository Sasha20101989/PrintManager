using PrintManager.Application.DefaultValues;
using PrintManager.Application.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Application.Services;

public class BranchService(IBranchStore branchStore) : IBranchService
{
    public Branch? GetById(int branchId)
    {
        return branchStore.GetById(branchId);
    }

    public async Task<Branch?> GetByIdAsync(int branchId)
    {
        return await branchStore.GetByIdAsync(branchId);
    }

    public async Task<IReadOnlyList<Branch>> GetByPageAsync(int? page, int? pageSize)
    {
        int pageNumber = page.HasValue && page > 0 ? page.Value : DefaultPaginationValues.BranchDefaultPage;
        int size = pageSize.HasValue && pageSize > 0 ? pageSize.Value : DefaultPaginationValues.BranchDefaultPageSize;

        int skip = (pageNumber - 1) * size;

        return await branchStore.GetByPageAsync(skip, size);
    }
}
