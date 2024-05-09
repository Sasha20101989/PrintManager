using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintManager.Persistence.Entities;

[Table("tbd_Employees")]
public partial class EmployeeEntity
{
    [Key]
    public int EmployeeId { get; set; }

    [StringLength(100)]
    public string EmployeeName { get; set; } = null!;

    public int BranchId { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Employees")]
    public virtual BranchEntity Branch { get; set; } = null!;

    [InverseProperty("Employee")]
    public virtual ICollection<JobEntity> Jobs { get; set; } = [];
}