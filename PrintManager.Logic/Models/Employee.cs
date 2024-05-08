namespace PrintManager.Logic.Models;

public class Employee
{
    private Employee(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public static Employee Create(int id, string name)
    {
        return new Employee(id, name);
    }
}
