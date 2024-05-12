using PrintManager.Logic.Models;

namespace PrintManager.Application.DTOs;

public class JobDTO
{
    private JobDTO(Job job, PrinterDTO printer, EmployeeDTO employee, StatusDTO status)
    {
        JobId= job.JobId;
        PrintJobName = job.PrintJobName;
        PagesPrinted = job.PagesPrinted;
        Printer = printer;
        Employee = employee;
        Status = status;
    }

    public int JobId { get; }

    public string PrintJobName { get;} = null!;

    public int PagesPrinted { get;}

    public PrinterDTO Printer { get; }

    public EmployeeDTO Employee { get;}

    public StatusDTO Status { get; }

    public static JobDTO Create(Job job)
    {
        PrinterDTO printerDTO = PrinterDTO.Create(job.Printer);

        EmployeeDTO employeeDTO = EmployeeDTO.Create(job.Employee);

        StatusDTO statusDTO = StatusDTO.Create(job.Status);

        return new JobDTO(job, printerDTO, employeeDTO, statusDTO);
    }
}
