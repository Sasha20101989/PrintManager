using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Persistence.Entities;

[Table("tbd_Branches")]
public partial class BranchEntity
{
    [Key]
    public int BranchId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("Branch")]
    public virtual ICollection<InstallationEntity> Installations { get; set; } = [];

    [InverseProperty("Branch")]
    public virtual ICollection<PrinterEntity> Printers { get; set; } = [];
}