using PrintManager.Applpication.Interfaces;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;

namespace PrintManager.Applpication.Services;

public class JobService(IJobStore jobStore, IEmployeeService employeeService, IPrinterService printerService, IInstallationService installationService, ICSVService cSVService) : IJobService
{
    public async Task<Job> CreateAsync(Job newJob)
    {
        return await jobStore.CreateAsync(newJob);
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

    public IReadOnlyList<Job> ImportJobsFromCsvAsync(Stream csvStream)
    {
        return cSVService.ReadCSV<Job>(csvStream).ToList();
    }
}
