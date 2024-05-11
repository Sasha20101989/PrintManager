﻿using Microsoft.Extensions.Caching.Memory;
using PrintManager.Applpication.DefaultValues;
using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;
using System.Linq;

namespace PrintManager.Applpication.Services;

public class InstallationService(IInstallationStore installationStore, IInstallationMemoryCache installationMemoryCache) : IInstallationService
{
    public async Task<Installation> CreateAsync(string installationName, Branch branch, Printer printer, bool defaultInstallation, int? printerOrder)
    {
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

            if (printerOrder > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(printerOrder), "The printer order exceeds the maximum allowed value (255).");
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

        installationMemoryCache.RemoveInstallations();

        return await installationStore.CreateAsync(newInstallation);
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

    public async Task<IReadOnlyList<Installation>> GetByBranchNameAsync(string branchName, int? page, int? pageSize)
    {
        int pageNumber = page.HasValue && page > 0 ? page.Value : DefaultPaginationValues.InstallationDefaultPage;
        int size = pageSize.HasValue && pageSize > 0 ? pageSize.Value : DefaultPaginationValues.InstallationDefaultPageSize;
        int skip = (pageNumber - 1) * size;

        IEnumerable<Installation> cachedInstallations = installationMemoryCache
            .GetInstallations()
            .Where(p => p.Branch?.BranchName == branchName)
            .Skip(skip)
            .Take(size);

        if (cachedInstallations is not null)
        {
            return cachedInstallations.ToList();
        }

        return await installationStore.GetByPageAsync(skip, size, branchName);
    }

    public async Task<Installation?> GetByIdAsync(int id)
    {
        Installation? cachedInstallation = installationMemoryCache
            .GetInstallations()
            .FirstOrDefault(i => i.InstallationId == id);

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
