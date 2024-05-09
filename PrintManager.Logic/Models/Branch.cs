namespace PrintManager.Logic.Models;

public class Branch
{
    private Branch(int id, string name, string? location)
    {
        Id = id;
        Name = name;
        Location = location;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public string? Location { get; private set; }

    public static Branch Create(int id, string name, string? location)
    {
        return new Branch(id, name, location);
    }
}
