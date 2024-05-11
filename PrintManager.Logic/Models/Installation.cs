using System.ComponentModel.DataAnnotations;

namespace PrintManager.Logic.Models;

public class Installation
{
    public int InstallationId { get; set; }

    public string InstallationName { get; set; } = null!;

    public int BranchId { get; set; }

    public int PrinterId { get; set; }

    public bool DefaultInstallation { get; set; }

    public int? PrinterOrder { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Printer Printer { get; set; } = null!;
}
