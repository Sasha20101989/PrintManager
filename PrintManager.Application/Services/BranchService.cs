using PrintManager.Application.DefaultValues;
using PrintManager.Application.Interfaces;
using PrintManager.Application.Parameters;
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
        PaginationParameters paginationParameters = PaginationParameters.Create(page, pageSize, DefaultPaginationValues.BranchDefaultPage, DefaultPaginationValues.BranchDefaultPageSize);

        return await branchStore.GetByPageAsync(paginationParameters.Skip, paginationParameters.Size);
    }
}
