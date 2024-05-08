using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Persistence.Entities;

[Table("tbd_Statuses")]
public partial class StatusEntity
{
    [Key]
    public int StatusId { get; set; }

    [StringLength(50)]
    public string StatusName { get; set; } = null!;

    [InverseProperty("Status")]
    public virtual ICollection<PrintSessionEntity> PrintSessions { get; set; } = [];
}