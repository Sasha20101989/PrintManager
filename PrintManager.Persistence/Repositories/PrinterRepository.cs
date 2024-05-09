using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Repositories;

public class PrinterRepository(PrintingManagementContext context, IMapper mapper) : IPrinterStore
{
    public async Task<IReadOnlyList<Printer>> GetAllAsync(string? connectionType = null)
    {
        IQueryable<PrinterEntity> printersQuery = context.Printers
            .AsNoTracking()
            .Include(p => p.ConnectionType);

        if (!string.IsNullOrWhiteSpace(connectionType))
        {
            printersQuery = printersQuery.Where(p => p.ConnectionType.ConnectionTypeName == connectionType);
        }

        return await printersQuery
            .Select(p => Printer.Create(p.PrinterId, p.PrinterName, p.ConnectionTypeId, p.Macaddress, p.DefaultPrinter))
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Printer>> GetByPageAsync(int skip, int pageSize, string? connectionType = null)
    {
        IQueryable<PrinterEntity> printersQuery = context.Printers
            .AsNoTracking()
            .Include(p => p.ConnectionType);

        if (!string.IsNullOrWhiteSpace(connectionType))
        {
            printersQuery = printersQuery.Where(p => p.ConnectionType.ConnectionTypeName == connectionType);
        }

        return await printersQuery
            .Skip(skip)
            .Take(pageSize)
            .Select(p => Printer.Create(p.PrinterId, p.PrinterName, p.ConnectionTypeId, p.Macaddress, p.DefaultPrinter))
            .ToListAsync();
    }
}
