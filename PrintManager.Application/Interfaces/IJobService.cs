using PrintManager.Logic.Models;

namespace PrintManager.Application.Interfaces;

public interface IJobService
{
    Task<Job> CreateAsync(Job newJob);

    Task<Job> GenerateAsync(string printJobName, int employeeId, int pagesPrinted, int? printerId);

    Job? GetById(int id);

    Task<Job?> GetByIdAsync(int id);

    IReadOnlyList<Job> ImportJobsFromCsvAsync(Stream csvStream);
}
