namespace PrintManager.Logic.Models;

public class Branch
{
    private Branch(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public static Branch Create(int id, string name)
    {
        return new Branch(id, name);
    }
}
