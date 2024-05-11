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
            .Select(p => mapper.Map<Printer>(p))
            .ToListAsync();
    }

    public async Task<Printer?> GetByIdAsync(int printerId)
    {
        PrinterEntity? printer = await context.Printers
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PrinterId == printerId);

        return mapper.Map<Printer>(printer);
    }

    public async Task<IReadOnlyList<Printer>> GetByPageAsync(int skip, int pageSize, Logic.Enums.ConnectionType? connectionType)
    {
        IQueryable<PrinterEntity> printersQuery = context.Printers
            .AsNoTracking()
            .Include(p => p.ConnectionType);

        if (connectionType.HasValue)
        {
            printersQuery = printersQuery.Where(p => p.ConnectionType.ConnectionTypeName == connectionType.ToString());
        }

        return await printersQuery
            .Skip(skip)
            .Take(pageSize)
            .Select(p => mapper.Map<Printer>(p))
            .ToListAsync();
    }
}
