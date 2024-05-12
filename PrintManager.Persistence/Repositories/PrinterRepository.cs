using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;
using PrintManager.Persistence.Entities;
using System.Reflection;

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

    public Printer? GetById(int printerId)
    {
        PrinterEntity? printer = context.Printers
             .AsNoTracking()
             .FirstOrDefault(p => p.PrinterId == printerId);

        return mapper.Map<Printer>(printer);
    }

    public async Task<IReadOnlyList<Printer>> GetByPageAsync(int skip, int pageSize, Logic.Enums.ConnectionTypes? connectionType)
    {
        IQueryable<PrinterEntity> printersQuery = context.Printers
            .Include(p => p.ConnectionType)
            .AsNoTracking();

        if (connectionType.HasValue)
        {
            printersQuery = printersQuery
            .Where(p => p.ConnectionType.ConnectionTypeName == connectionType.ToString());
        }

        return await printersQuery
            .Skip(skip)
            .Take(pageSize)
            .Select(p => mapper.Map<Printer>(p))
            .ToListAsync();
    }
}
