namespace PrintManager.Logic.Models;

public class Printer
{
    private Printer(int id, string name, int connectionTypeId, string? macaddress, bool defaultPrinter)
    {
        Id = id;
        PrinterName = name;
        ConnectionTypeId = connectionTypeId;
        Macaddress = macaddress;
        DefaultPrinter = defaultPrinter;
    }

    public int Id { get; private set; }

    public string PrinterName { get; private set; }

    public int ConnectionTypeId { get; private set; }

    public string? Macaddress { get; private set; }

    public bool DefaultPrinter { get; private set; }

    public static Printer Create(int id, string name, int connectionTypeId, string? macaddress, bool defaultPrinter)
    {
        return new Printer(id, name, connectionTypeId, macaddress, defaultPrinter);
    }
}
