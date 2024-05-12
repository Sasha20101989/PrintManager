using PrintManager.Application.DefaultValues;
using PrintManager.Application.Interfaces;
using PrintManager.Application.Parameters;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Application.Services;

public class InstallationService(
    IInstallationStore installationStore, 
    IInstallationMemoryCache installationMemoryCache,
    IBranchService branchService,
    IPrinterService printerService) : IInstallationService
{
    public async Task<Installation> CreateAsync(string installationName, int branchId, int printerId, bool defaultInstallation, int? printerOrder)
    {
        Branch? branch = await branchService.GetByIdAsync(branchId) ?? 
            throw new ArgumentNullException($"{nameof(Branch)} with id {branchId} not found.");

        Printer? printer = await printerService.GetByIdAsync(printerId) ??
            throw new ArgumentNullException($"{nameof(Printer)} with id {printerId} not found.");

        if (!printerOrder.HasValue)
        {
            bool isFirstInstallation = await installationStore.IsFirstInstallationInBranchAsync(installationName, branch.BranchId);

            if (isFirstInstallation)
            {
                printerOrder = 1;
            }
        }

        if (printerOrder.HasValue)
        {
            Installation? existingInstallation = await installationStore.GetByProperties(installationName, branch.BranchId, printer.PrinterId, printerOrder.Value);

            if (existingInstallation is not null)
            {
                throw new ArgumentException($"An installation with printer order {printerOrder} already exists.");
            }
        }
        else
        {
            int? maxCurrentOrder = await installationStore.GetMaxPrinterOrderByInstallationNameAsync(installationName, branch.BranchId);

            printerOrder = maxCurrentOrder is null ? 1 : maxCurrentOrder + 1;

            if (printerOrder > DefaultPrinterValues.PrinterMaxOrder)
            {
                throw new ArgumentOutOfRangeException(nameof(printerOrder), $"The printer order exceeds the maximum allowed value ({DefaultPrinterValues.PrinterMaxOrder}).");
            }
        }

        if (defaultInstallation)
        {
            bool defaultInstallationExists = await installationStore.DefaultInstallationExistsInBranchAsync(installationName, branch.BranchId);
            
            if (defaultInstallationExists)
            {
                throw new ArgumentException($"A default installation already exists in branch {branch.BranchName}.");
            }
        }

        Installation newInstallation = new()
        {
            InstallationName = installationName,
            BranchId = branch.BranchId,
            PrinterId = printer.PrinterId,
            DefaultInstallation = defaultInstallation,
            PrinterOrder = printerOrder
        };
      
        Installation createdInstallation = await installationStore.CreateAsync(newInstallation);

        createdInstallation.Printer = printer;

        createdInstallation.Branch = branch;

        installationMemoryCache.RemoveInstallations();

        return createdInstallation;
    }

    public async Task DeleteAsync(Installation installationToDelete)
    {
        if (installationToDelete.DefaultInstallation)
        {
            bool defaultInstallationExists = await installationStore.DefaultInstallationExistsInBranchAsync(installationToDelete.InstallationName, installationToDelete.BranchId);

            if (!defaultInstallationExists)
            {
                throw new InvalidOperationException($"Deleting installation with id {installationToDelete.InstallationId} would leave the branch without a default installation.");
            }
        }

        await installationStore.DeleteAsync(installationToDelete.InstallationId);

        installationMemoryCache.RemoveInstallations();
    }

    public async Task<IReadOnlyList<Installation>> GetByBranchNameAsync(string branchName, int? page = null, int? pageSize = null)
    {
        PaginationParameters paginationParameters = PaginationParameters.Create(page, pageSize, DefaultPaginationValues.InstallationDefaultPage, DefaultPaginationValues.InstallationDefaultPageSize);

        IReadOnlyList<Installation>? cachedInstallations = installationMemoryCache.GetInstallationsByPage(paginationParameters.Skip, paginationParameters.Size, branchName);

        if (cachedInstallations is not null)
        {
            return cachedInstallations;
        }

        IReadOnlyList<Installation> installations = await installationStore.GetByPageAsync(paginationParameters.Skip, paginationParameters.Size, branchName);

        installationMemoryCache.SetInstallations(installations);

        return installations;
    }


    public async Task<Installation> GetDefaultInstallationByBranchNameAsync(string branchName)
    {
        IReadOnlyList<Installation> installations = await GetByBranchNameAsync(branchName);

        Installation defaultInstallation = installations.FirstOrDefault(i => i.DefaultInstallation) ??
            throw new ArgumentNullException($"{nameof(Installation.DefaultInstallation)} in {nameof(Branch)} ({branchName}) not found in {nameof(Installation)}s.");

        return defaultInstallation;
    }


    public Installation? GetById(int id)
    {
        Installation? cachedInstallation = installationMemoryCache.GetInstallationById(id);

        if (cachedInstallation is not null)
        {
            return cachedInstallation;
        }

        return installationStore.GetById(id);
    }

    public async Task<Installation?> GetByIdAsync(int id)
    {
        Installation? cachedInstallation = installationMemoryCache.GetInstallationById(id);

        if (cachedInstallation is not null)
        {
            return cachedInstallation;
        }

        return await installationStore.GetByIdAsync(id);
    }

    public async Task<Installation?> GetByProperties(string installanionName, int branchId, int printerId, int printerOrder)
    {
        Installation? cachedInstallation = installationMemoryCache
            .GetInstallations()
            .FirstOrDefault(i => 
                i.InstallationName == installanionName && 
                i.BranchId == branchId && 
                i.PrinterId == printerId &&
                i.PrinterOrder == printerOrder);

        if (cachedInstallation is not null)
        {
            return cachedInstallation;
        }

        return await installationStore.GetByProperties(installanionName, branchId, printerId, printerOrder);
    }
}
