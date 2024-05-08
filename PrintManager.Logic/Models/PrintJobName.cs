namespace PrintManager.Logic.Models;

public class PrintJobName
{
    private PrintJobName(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public static PrintJobName Create(int id, string name) 
    { 
        return new PrintJobName(id, name);
    }
}
