using PrintManager.Applpication.DefaultValues;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Enums;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;
using System.Drawing;

namespace PrintManager.Applpication.Services;

public class InstallationService(IInstallationStore installationStore) : IInstallationService
{
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
