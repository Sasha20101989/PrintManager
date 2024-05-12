using PrintManager.Application.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Application.Services;

public class StatusService(IStatusStore statusStore) : IStatusService
{
    public async Task<Status?> GetByIdAsync(int id)
    {
        return await statusStore.GetByIdAsync(id);
    }
}
