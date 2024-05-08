using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Applpication.Services;

public class BranchService(IBranchStore branchStore) : IBranchService
{
    public async Task<IReadOnlyList<Branch>> GetAllAsync()
    {
        return await branchStore.GetAllAsync();
    }
}
