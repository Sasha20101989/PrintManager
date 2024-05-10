using PrintManager.Applpication.DefaultValues;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Applpication.Services;

public class InstallationService(IInstallationStore installationStore, IBranchService branchService) : IInstallationService
{
    public async Task<Installation> CreateAsync(string installationName, Branch branch, Printer printer, bool defaultInstallation, int? printerOrder)
    {
        if (printerOrder.HasValue)
        {
            //bool isOrderExists = await installationStore.IsPrinterOrderExistsAsync(branchId, printerOrder.Value);

            //if (isOrderExists)
            //{
            //    throw new ArgumentException($"Installation with printer order {printerOrder} already exists in branch {branch.BranchName}.");
            //}
        }
        else
        {
            int? maxCurrentOrder = await installationStore.GetMaxPrinterOrderByInstallationNameAsync(installationName);

            //int nextOrder = await installationStore.GetNextPrinterOrderAsync(branch.BranchId);

            printerOrder = maxCurrentOrder is null ? 1 : maxCurrentOrder + 1;
        }

        Installation newInstallation = new()
        {
            InstallationName = installationName,
            BranchId = branch.BranchId,
            PrinterId = printer.PrinterId,
            DefaultInstallation = defaultInstallation,
            PrinterOrder = printerOrder
        };

        return await installationStore.CreateAsync(newInstallation);
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

    public async Task<Installation?> GetByProperties(string installanionName, int branchId, int printerId, int printerOrder)
    {
        return await installationStore.GetByProperties(installanionName, branchId, printerId, printerOrder);
    }
}
