namespace PrintManager.Logic.Models;

public class Job
{
    public int JobId { get; set; }

    public string PrintJobName { get; set; } = null!;

    public int PagesPrinted { get; set; }

    public int? PrinterId { get; set; }

    public int EmployeeId { get; set; }

    public int? StatusId { get; set; }

    public virtual Printer Printer { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual Status? Status { get; set; }
}
