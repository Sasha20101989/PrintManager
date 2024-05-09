namespace PrintManager.Logic.Models;

public class Job
{
    private Job(int id, int printerId, int employeeId, string printJobName, int pagesPrinted, int statusId)
    {
        Id = id;
        PrinterId = printerId;
        EmployeeId = employeeId;
        PrintJobName = printJobName;
        PagesPrinted = pagesPrinted;
        StatusId = statusId;
    }

    public int Id { get; private set; }

    public int PrinterId { get; set; }

    public int EmployeeId { get; set; }

    public string PrintJobName { get; set; }

    public int PagesPrinted { get; set; }

    public int StatusId { get; set; }

    public static Job Create(int id, int printerId, int employeeId, string printJobName, int pagesPrinted, int statusId)
    {
        return new Job(id, printerId, employeeId, printJobName, pagesPrinted, statusId);
    }
}
