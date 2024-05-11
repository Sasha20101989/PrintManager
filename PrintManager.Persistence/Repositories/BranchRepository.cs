using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Repositories;

public class BranchRepository(PrintingManagementContext context, IMapper mapper) : IBranchStore
{
    public async Task<IReadOnlyList<Branch>> GetAllAsync()
    {
        IReadOnlyList<Branch> branches = await context.Branches
            .AsNoTracking()
            .Select(b => mapper.Map<Branch>(b))
            .ToListAsync();

        return branches;
    }

    public Branch? GetById(int branchId)
    {
        BranchEntity? branch = context.Branches
             .AsNoTracking()
             .FirstOrDefault(b => b.BranchId == branchId);

        return mapper.Map<Branch>(branch);
    }

    public async Task<Branch?> GetByIdAsync(int branchId)
    {
        BranchEntity? branch = await context.Branches
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.BranchId == branchId);

        return mapper.Map<Branch>(branch);
    }

    public async Task<IReadOnlyList<Branch>> GetByPageAsync(int skip, int pageSize)
    {
        return await context.Branches
            .AsNoTracking()
            .Skip(skip)
            .Take(pageSize)
            .Select(b => mapper.Map<Branch>(b))
            .ToListAsync();
    }
}