using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Repositories;

public class PrinterRepository(PrintingManagementContext context, IMapper mapper) : IPrinterStore
{
    public async Task<IReadOnlyList<Printer>> GetAllAsync(Logic.Enums.ConnectionType connectionType)
    {
        var printersQuery = await context.Printers.ToListAsync();
        IQueryable<StatusEntity> statusesQuery = context.Statuses;

        //printersQuery = printersQuery.Where(p => p.ConnectionTypeId == (int)connectionType);

        return null;
    }
}
