using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintManager.Persistence.Entities;

[Table("tbd_Printers")]
public partial class PrinterEntity
{
    [Key]
    [Column("PrinterId")]
    public int PrinterId { get; set; }

    [StringLength(100)]
    public string PrinterName { get; set; } = null!;

    public int ConnectionTypeId { get; set; }

    [Column("MACAddress")]
    [StringLength(17)]
    public string? Macaddress { get; set; } = null!;

    public bool DefaultPrinter { get; set; }

    [ForeignKey("ConnectionTypeId")]
    [InverseProperty("Printers")]
    public virtual ConnectionTypeEntity ConnectionType { get; set; } = null!;

    [InverseProperty("Printer")]
    public virtual ICollection<InstallationEntity> Installations { get; set; } = [];

    [InverseProperty("Printer")]
    public virtual ICollection<JobEntity> Jobs { get; set; } = [];
}