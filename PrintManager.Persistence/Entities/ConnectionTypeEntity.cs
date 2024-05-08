using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Persistence.Entities;

[Table("tbd_ConnectionTypes")]
public partial class ConnectionTypeEntity
{
    [Key]
    public int ConnectionTypeId { get; set; }

    [StringLength(50)]
    public string TypeName { get; set; } = null!;

    [InverseProperty("ConnectionType")]
    public virtual ICollection<PrinterEntity> Printers { get; set; } = [];
}