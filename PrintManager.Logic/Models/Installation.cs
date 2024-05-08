namespace PrintManager.Logic.Models;

public class Installation
{
    private Installation(int id, int branchId, int printerId, bool isDefault)
    {
        Id = id;
        BranchId = branchId;
        PrinterId = printerId;
        IsDefault = isDefault;
    }

    public int Id { get; private set; }

    public int BranchId { get; private set; }

    public int PrinterId { get; private set; }

    public bool IsDefault { get; private set; }

    public static Installation Create(int id, int branchId, int printerId, bool isDefault)
    {
        return new Installation(id, branchId, printerId, isDefault);
    }
}
