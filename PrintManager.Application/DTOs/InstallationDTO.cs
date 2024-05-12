using PrintManager.Logic.Models;

namespace PrintManager.Application.DTOs;

public class InstallationDTO
{
    private InstallationDTO(Installation installation, BranchDTO branch, PrinterDTO printer)
    {
        InstallationId = installation.InstallationId;
        InstallationName = installation.InstallationName;
        DefaultInstallation = installation.DefaultInstallation;
        PrinterOrder = installation.PrinterOrder;
        Branch = branch;
        Printer = printer;
    }

    public int InstallationId { get; }

    public string InstallationName { get; } = null!;

    public bool DefaultInstallation { get; }

    public int? PrinterOrder { get; }

    public virtual BranchDTO Branch { get; } = null!;

    public virtual PrinterDTO Printer { get; } = null!;

    public static InstallationDTO Create(Installation installation)
    {
        BranchDTO branchDTO = BranchDTO.Create(installation.Branch);

        PrinterDTO printerDTO = PrinterDTO.Create(installation.Printer);

        return new InstallationDTO(installation, branchDTO, printerDTO);
    }
}
