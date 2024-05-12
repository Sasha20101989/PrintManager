using PrintManager.Logic.Models;

namespace PrintManager.Application.DTOs
{
    public class PrinterDTO
    {
        private PrinterDTO(Printer printer, ConnectionTypeDTO connectionType)
        {
            PrinterId = printer.PrinterId;
            PrinterName = printer.PrinterName;
            Macaddress = printer.Macaddress;
            DefaultPrinter = printer.DefaultPrinter;
            ConnectionType = connectionType;
        }

        public int PrinterId { get; }

        public string? PrinterName { get; }

        public ConnectionTypeDTO ConnectionType { get; }

        public string? Macaddress { get; }

        public bool DefaultPrinter { get; }

        public static PrinterDTO Create(Printer printer)
        {
            ConnectionTypeDTO connectionTypeDTO = ConnectionTypeDTO.Create(printer.ConnectionType);

            return new PrinterDTO(printer, connectionTypeDTO);
        }
    }
}
