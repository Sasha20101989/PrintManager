using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintManager.Persistence.Entities;

[Table("tbd_Jobs")]
public partial class JobEntity
{
    [Key]
    public int JobId { get; set; }

    public int PrinterId { get; set; }

    public int EmployeeId { get; set; }

    [StringLength(100)]
    public string PrintJobName { get; set; } = null!;

    public int PagesPrinted { get; set; }

    public int StatusId { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Jobs")]
    public virtual EmployeeEntity Employee { get; set; } = null!;

    [ForeignKey("PrinterId")]
    [InverseProperty("Jobs")]
    public virtual PrinterEntity Printer { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Jobs")]
    public virtual StatusEntity Status { get; set; } = null!;
}