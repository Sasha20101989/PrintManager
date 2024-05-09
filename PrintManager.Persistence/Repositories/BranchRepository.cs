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
            .Select(b => Branch.Create(b.BranchId, b.BranchName, b.Location))
            .ToListAsync();

        return branches;
    }

    public async Task<IReadOnlyList<Branch>> GetByPageAsync(int skip, int pageSize)
    {
        return await context.Branches
            .AsNoTracking()
            .Skip(skip)
            .Take(pageSize)
            .Select(b => Branch.Create(b.BranchId, b.BranchName, b.Location))
            .ToListAsync();
    }
}