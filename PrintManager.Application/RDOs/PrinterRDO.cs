using PrintManager.Logic.Models;

namespace PrintManager.Application.RDOs
{
    public class PrinterRDO
    {
        private PrinterRDO(int printerId, string? printerName, string? macaddress, bool defaultPrinter, ConnectionType connectionType)
        {
            PrinterId = printerId;
            PrinterName = printerName;
            Macaddress = macaddress;
            DefaultPrinter = defaultPrinter;
            ConnectionType = connectionType;
        }

        public int PrinterId { get; private set; }

        public string? PrinterName { get; private set; }

        public ConnectionType ConnectionType { get; private set; }

        public string? Macaddress { get; private set; }

        public bool DefaultPrinter { get; private set; }

        public static PrinterRDO Create(int printerId, string? printerName, string? macaddress, bool defaultPrinter, ConnectionType connectionType)
        {
            return new PrinterRDO(printerId, printerName, macaddress, defaultPrinter, connectionType);
        }
    }
}
