using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Persistence.Entities;

[Table("tbd_PrintJobNames")]
public partial class PrintJobNameEntity
{
    [Key]
    public int PrintJobId { get; set; }

    [StringLength(100)]
    public string PrintJobName { get; set; } = null!;

    [InverseProperty("PrintJob")]
    public virtual ICollection<PrintSessionEntity> PrintSessions { get; set; } = [];
}