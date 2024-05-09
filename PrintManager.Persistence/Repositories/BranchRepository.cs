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
            .Select(b => mapper.Map<Branch>(b))
            .ToListAsync();

        return branches;
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