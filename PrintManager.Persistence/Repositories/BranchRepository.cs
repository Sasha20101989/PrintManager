using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Persistence.Repositories;

public class BranchRepository(PrintingManagementContext context, IMapper mapper) : IBranchStore
{
    public async Task<IReadOnlyList<Branch>> GetAllAsync()
    {
        IReadOnlyList<Branch> branches = await context.Branches
            .AsNoTracking()
            .Select(b => Branch.Create(b.BranchId, b.Name))
            .ToListAsync();

        return branches;
    }
}