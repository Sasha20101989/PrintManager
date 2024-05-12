using PrintManager.Logic.Enums;
using System.Data;

namespace PrintManager.Logic.Models;

public class Printer
{
    public int PrinterId { get; set; }

    public string? PrinterName { get; set; }

    public string? Macaddress { get; set; }

    public bool DefaultPrinter { get; set; }

    public int ConnectionTypeId { get; set; }

    public ConnectionType ConnectionType { get; set; }
}
