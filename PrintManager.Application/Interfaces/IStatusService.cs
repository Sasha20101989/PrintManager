
using PrintManager.Logic.Models;

namespace PrintManager.Application.Interfaces;

public interface IStatusService
{
    Task<Status?> GetByIdAsync(int id);
}
