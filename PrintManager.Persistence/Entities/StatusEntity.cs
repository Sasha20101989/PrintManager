using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintManager.Persistence.Entities;

[Table("tbd_Statuses")]
public partial class StatusEntity
{
    [Key]
    public int StatusId { get; set; }

    [StringLength(10)]
    public string StatusName { get; set; } = null!;

    [InverseProperty("Status")]
    public virtual ICollection<JobEntity> Jobs { get; set; } = [];
}