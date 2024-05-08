using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Persistence.Entities;

[Table("tbd_Printers")]
public partial class PrinterEntity
{
    [Key]
    public int PrinterId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public int ConnectionTypeId { get; set; }

    [Column("MACAddress")]
    [StringLength(50)]
    public string? Macaddress { get; set; }

    public bool DefaultPrinter { get; set; }

    public int BranchId { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Printers")]
    public virtual BranchEntity Branch { get; set; } = null!;

    [ForeignKey("ConnectionTypeId")]
    [InverseProperty("Printers")]
    public virtual ConnectionTypeEntity ConnectionType { get; set; } = null!;

    [InverseProperty("Printer")]
    public virtual ICollection<InstallationEntity> Installations { get; set; } = [];

    [InverseProperty("Printer")]
    public virtual ICollection<PrintSessionEntity> PrintSessions { get; set; } = [];
}