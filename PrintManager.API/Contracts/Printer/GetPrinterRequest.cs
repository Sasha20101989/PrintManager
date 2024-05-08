using PrintManager.Logic.Enums;

namespace PrintManager.API.Contracts.Printer
{
    public record GetPrinterRequest(ConnectionType ConnectionType);
}
