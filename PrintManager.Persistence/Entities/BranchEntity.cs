using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintManager.Persistence.Entities;

[Table("tbd_Branches")]
public partial class BranchEntity
{
    [Key]
    public int BranchId { get; set; }

    [StringLength(100)]
    public string BranchName { get; set; } = null!;

    [StringLength(100)]
    public string? Location { get; set; } = null!;

    [InverseProperty("Branch")]
    public virtual ICollection<EmployeeEntity> Employees { get; set; } = [];

    [InverseProperty("Branch")]
    public virtual ICollection<InstallationEntity> Installations { get; set; } = [];
}