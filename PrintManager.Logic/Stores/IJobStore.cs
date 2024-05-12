using PrintManager.Logic.Models;

namespace PrintManager.Logic.Stores;

public interface IJobStore
{
    Task<Job> CreateJobAsync(Job job);

    Job CreateJob(Job job);

    Job? GetById(int id);

    Task<Job?> GetByIdAsync(int id);
}
