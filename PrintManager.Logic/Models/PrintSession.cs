namespace PrintManager.Logic.Models;

public class PrintSession
{
    private PrintSession(int id, int printerId, int employeeId, int printJobId, int pagesPrinted, int statusId)
    {
        Id = id;
        PrinterId = printerId;
        EmployeeId = employeeId;
        PrintJobId = printJobId;
        PagesPrinted = pagesPrinted;
        StatusId = statusId;
    }

    public int Id { get; private set; }

    public int PrinterId { get; private set; }

    public int EmployeeId { get; private set; }

    public int PrintJobId { get; private set; }

    public int PagesPrinted { get; private set; }

    public int StatusId { get; private set; }

    public static PrintSession Create(int id, int printerId, int employeeId, int printJobId, int pagesPrinted, int statusId)
    {
        return new PrintSession(id, printerId, employeeId, printJobId, pagesPrinted, statusId);
    }
}
