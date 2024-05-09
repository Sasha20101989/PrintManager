namespace PrintManager.Logic.Models;

public class Printer
{
    public int PrinterId { get; private set; }

    public string? PrinterName { get; private set; }

    public int ConnectionTypeId { get; private set; }

    public string? Macaddress { get; private set; }

    public bool DefaultPrinter { get; private set; }
}
