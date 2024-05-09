using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Repositories;

public class InstallationRepository(PrintingManagementContext context, IMapper mapper) : IInstallationStore
{
    public async Task<Installation> CreateAsync(Installation installation)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        await context.Installations
            .Where(i => i.InstallationId == id)
            .ExecuteDeleteAsync();
    }

    public async Task<Installation?> GetByIdAsync(int id)
    {
        InstallationEntity? installation = await context.Installations
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.InstallationId == id);

        return mapper.Map<Installation>(installation);
    }

    public async Task<IReadOnlyList<Installation>> GetByPageAsync(int skip, int pageSize, string branchName)
    {
        IQueryable<InstallationEntity> installationsQuery = context.Installations
            .AsNoTracking()
            .Include(p => p.Branch);

        if (!string.IsNullOrWhiteSpace(branchName))
        {
            installationsQuery = installationsQuery.Where(p => p.Branch.BranchName == branchName);
        }

        return await installationsQuery
            .Skip(skip)
            .Take(pageSize)
            .Select(p => mapper.Map<Installation>(p))
            .ToListAsync();
    }
}
