using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Repositories;

public class StatusRepository(PrintingManagementContext context, IMapper mapper) : IStatusStore
{
    public async Task<Status?> GetByIdAsync(int id)
    {
        StatusEntity? statusEntity = await context.Statuses
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.StatusId == id);

        return mapper.Map<Status>(statusEntity);
    }
}
