using PrintManager.Application.Contracts.Job;
using PrintManager.Logic.Models;

namespace PrintManager.Application.Interfaces;

public interface IJobService
{
    Task<Job> CreateJobAsync(Job newJob);

    Task<Job> GenerateAsync(string printJobName, int employeeId, int pagesPrinted, int? printerId);

    Job? GetById(int id);

    Task<Job?> GetByIdAsync(int id);

    IReadOnlyList<CsvJobData> ImportJobsFromCsv(Stream csvStream);

    Task<Job> CreateJobFromRecordAsync(CsvJobData record);

    bool ValidateRecord(CsvJobData record);

    Task SimulatePrintingAsync(Job generatedJob);
}
