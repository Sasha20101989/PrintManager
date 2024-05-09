namespace PrintManager.Logic.Models;

public class Installation
{
    public int InstallationId { get; set; }

    public string? InstallationName { get; set; }

    public int BranchId { get; set; }

    public int PrinterId { get; set; }

    public bool DefaultInstallation { get; set; }

    public int? PrinterOrder { get; set; }
}
