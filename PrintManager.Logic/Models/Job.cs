namespace PrintManager.Logic.Models;

public class Job
{
    public int JobId { get; set; }

    public int? PrinterId { get; set; }

    public int EmployeeId { get; set; }

    public string PrintJobName { get; set; } = null!;

    public int PagesPrinted { get; set; }

    public int? StatusId { get; set; }
}
