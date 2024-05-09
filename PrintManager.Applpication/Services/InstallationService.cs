using PrintManager.Applpication.DefaultValues;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Applpication.Services;

public class InstallationService(IInstallationStore installationStore) : IInstallationService
{
    public async Task<Installation> CreateAsync(string installationName, int branchId, int printerId, bool defaultInstallation, int? printerOrder)
    {
        Installation installation = new()
        {
            InstallationName = installationName,
            BranchId = branchId,
            PrinterId = printerId,
            DefaultInstallation = defaultInstallation,
            PrinterOrder = printerOrder
        };

        return await installationStore.CreateAsync(installation);
    }

    public async Task DeleteAsync(int id)
    {
        await installationStore.DeleteAsync(id);
    }

    public async Task<IReadOnlyList<Installation>> GetByBranchNameAsync(string branchName, int? page, int? pageSize)
    {
        int pageNumber = page.HasValue && page > 0 ? page.Value : DefaultPaginationValues.PrinterDefaultPage;
        int size = pageSize.HasValue && pageSize > 0 ? pageSize.Value : DefaultPaginationValues.PrinterDefaultPageSize;
        int skip = (pageNumber - 1) * size;

        return await installationStore.GetByPageAsync(skip, size, branchName);
    }

    public async Task<Installation?> GetByIdAsync(int id)
    {
        return await installationStore.GetByIdAsync(id);
    }
}
