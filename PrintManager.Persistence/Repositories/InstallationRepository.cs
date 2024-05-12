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
        InstallationEntity installationEntity = mapper.Map<InstallationEntity>(installation);

        await context.Installations.AddAsync(installationEntity);

        await context.SaveChangesAsync();

        return mapper.Map<Installation>(installationEntity);
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
            .Include(i => i.Branch)
            .Include(i => i.Printer)
            .ThenInclude(p => p.ConnectionType)
            .FirstOrDefaultAsync(i => i.InstallationId == id);

        return mapper.Map<Installation>(installation);
    }


    public Installation? GetById(int id)
    {
        InstallationEntity? installation = context.Installations
            .AsNoTracking()
            .Include(i => i.Branch)
            .Include(i => i.Printer)
            .ThenInclude(p => p.ConnectionType)
            .FirstOrDefault(i => i.InstallationId == id);

        return mapper.Map<Installation>(installation);
    }

    public async Task<int?> GetMaxPrinterOrderByInstallationNameAsync(string installationName, int branchId)
    {
        return await context.Installations
            .AsNoTracking()
            .Where(i => installationName == i.InstallationName && i.BranchId == branchId)
            .MaxAsync(inst => inst.PrinterOrder);
    }

    public async Task<bool> DefaultInstallationExistsInBranchAsync(string installationName, int branchId)
    {
        return await context.Installations.AnyAsync(i => i.InstallationName == installationName && i.BranchId == branchId && i.DefaultInstallation);
    }

    public async Task<bool> IsFirstInstallationInBranchAsync(string installationName, int branchId)
    {
        return !await context.Installations.AnyAsync(i => i.InstallationName == installationName && i.BranchId == branchId);
    }

    public async Task<IReadOnlyList<Installation>> GetByPageAsync(int skip, int pageSize, string branchName)
    {
        IQueryable<InstallationEntity> installationsQuery = context.Installations
            .AsNoTracking()
            .Include(p => p.Branch)
            .Include(p => p.Printer);

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

    public async Task<Installation?> GetByProperties(string installationName, int branchId, int printerId, int printerOrder)
    {
        InstallationEntity? installation = await context.Installations
            .AsNoTracking()
            .FirstOrDefaultAsync(i =>
               installationName == i.InstallationName &&
               branchId == i.BranchId &&
               printerId == i.PrinterId &&
               printerOrder == i.PrinterOrder);

        return mapper.Map<Installation>(installation);
    }
}
