using PrintManager.Application.Contracts.Job;
using PrintManager.Application.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Application.Services;

public class JobService(IJobStore jobStore, IEmployeeService employeeService, IPrinterService printerService, IInstallationService installationService, ICSVService cSVService) : IJobService
{
    private readonly Random _random = new();

    public async Task<Job> CreateJobAsync(Job newJob)
    {
        return await jobStore.CreateJobAsync(newJob);
    }

    public async Task<Job> CreateJobFromRecordAsync(CsvJobData record)
    {
        if (string.IsNullOrWhiteSpace(record.PrintJobName) ||
            record.EmployeeId is null ||
            record.PrinterId is null ||
            record.PagesPrinted is null)
        {
            throw new ArgumentNullException();
        }

        Job newJob = await GenerateAsync(record.PrintJobName, record.EmployeeId.Value, record.PagesPrinted.Value, record.PrinterId.Value);

        newJob.StatusId = IsPrintSuccess() ?
            (int)Logic.Enums.Status.Success :
            (int)Logic.Enums.Status.Failure;

        return await CreateJobAsync(newJob);
    }

    public async Task<Job> GenerateAsync(string printJobName, int employeeId, int pagesPrinted, int? printerId)
    {
        Employee? employee = await employeeService.GetByIdAsync(employeeId) ??
            throw new ArgumentNullException($"{nameof(Employee)} with id {employeeId} not found.");

        Printer? printer;

        if (printerId.HasValue)
        {
            printer = await printerService.GetByIdAsync(printerId.Value) ??
                throw new ArgumentNullException($"{nameof(Printer)} with id {printerId} not found.");
        }
        else
        {
            Installation defaultInstallation = await installationService.GetDefaultInstallationByBranchNameAsync(employee.Branch.BranchName);

            printer = defaultInstallation.Printer;
        }
       
        Job job = new()
        {
            PrintJobName = printJobName,
            EmployeeId = employeeId,
            PagesPrinted = pagesPrinted,
            PrinterId = printer.PrinterId
        };

       return job;
    }

    public Job? GetById(int id)
    {
        return jobStore.GetById(id);
    }

    public async Task<Job?> GetByIdAsync(int id)
    {
        return await jobStore.GetByIdAsync(id);
    }

    public IReadOnlyList<CsvJobData> ImportJobsFromCsv(Stream csvStream)
    {
        return cSVService.ReadCSV<CsvJobData>(csvStream)
            .Where(ValidateRecord)
            .ToList();
    }

    public bool ValidateRecord(CsvJobData record)
    {
        ValidationContext context = new(record);

        return Validator.TryValidateObject(record, context, null, validateAllProperties: true);
    }

    public async Task SimulatePrintingAsync(Job generatedJob)
    {
        int delayMilliseconds = _random.Next(1000, 4001);

        await Task.Delay(delayMilliseconds);

        generatedJob.StatusId = IsPrintSuccess() ?
            (int)Logic.Enums.Status.Success :
            (int)Logic.Enums.Status.Failure;
    }

    private bool IsPrintSuccess()
    {
        int result = _random.Next(1, 3);

        return result == (int)Logic.Enums.Status.Success;
    }
}
