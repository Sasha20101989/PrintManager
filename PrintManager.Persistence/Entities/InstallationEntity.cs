using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintManager.Persistence.Entities;

[Table("tbd_Installations")]
public partial class InstallationEntity
{
    [Key]
    public int InstallationId { get; set; }

    public int BranchId { get; set; }

    public int PrinterId { get; set; }

    public bool IsDefault { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Installations")]
    public virtual BranchEntity Branch { get; set; } = null!;

    [ForeignKey("PrinterId")]
    [InverseProperty("Installations")]
    public virtual PrinterEntity Printer { get; set; } = null!;
}