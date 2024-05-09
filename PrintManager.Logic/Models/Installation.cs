namespace PrintManager.Logic.Models;

public class Installation
{
    private Installation(int id, string installationName, int branchId, int printerId, bool isDefault, int printerOrder)
    {
        Id = id;
        InstallationName = installationName;
        BranchId = branchId;
        PrinterId = printerId;
        IsDefault = isDefault;
        PrinterOrder = printerOrder;
    }

    public int Id { get; private set; }

    public string InstallationName { get; set; } = null!;

    public int BranchId { get; set; }

    public int PrinterId { get; set; }

    public bool IsDefault { get; set; }

    public int PrinterOrder { get; set; }

    public static Installation Create(int id, string installationName, int branchId, int printerId, bool isDefault, int printerOrder)
    {
        return new Installation(id, installationName, branchId, printerId, isDefault, printerOrder);
    }
}
