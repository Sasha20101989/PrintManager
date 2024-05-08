namespace PrintManager.Logic.Models;

public class Status
{
    private Status(int id, string statusName)
    {
        Id = id;
        StatusName = statusName;
    }

    public int Id { get; private set; }

    public string StatusName { get; private set; }

    public static Status Create(int id, string statusName)
    {
        return new Status(id, statusName);
    }
}
