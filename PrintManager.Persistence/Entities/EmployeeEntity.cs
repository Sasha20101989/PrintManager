using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Persistence.Entities;

[Table("tbd_Employees")]
public partial class EmployeeEntity
{
    [Key]
    public int EmployeeId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("Employee")]
    public virtual ICollection<PrintSessionEntity> PrintSessions { get; set; } = [];
}