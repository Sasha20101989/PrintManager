namespace PrintManager.Logic.Models;

public class Printer
{
    private Printer(int id, string name, int connectionTypeId, string macaddress, bool defaultPrinter, int branchId)
    {
        Id = id;
        Name = name;
        ConnectionTypeId = connectionTypeId;
        Macaddress = macaddress;
        DefaultPrinter = defaultPrinter;
        BranchId = branchId;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public int ConnectionTypeId { get; private set; }

    public string Macaddress { get; private set; }

    public bool DefaultPrinter { get; private set; }

    public int BranchId { get; private set; }

    public static Printer Create(int id, string name, int connectionTypeId, string macaddress, bool defaultPrinter, int branchId)
    {
        return new Printer(id, name, connectionTypeId, macaddress, defaultPrinter, branchId);
    }
}
