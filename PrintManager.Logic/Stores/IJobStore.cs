using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IJobStore
{
    Task<Job> CreateAsync(Job job);

    Job? GetById(int id);

    Task<Job?> GetByIdAsync(int id);
}
