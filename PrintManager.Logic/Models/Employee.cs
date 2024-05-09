namespace PrintManager.Logic.Models;

public class Employee
{
    private Employee(int id, string name, int branchId)
    {
        Id = id;
        Name = name;
        BranchId = branchId;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public int BranchId { get; private set; }

    public static Employee Create(int id, string name, int branchId)
    {
        return new Employee(id, name, branchId);
    }
}
