using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrintManager.Persistence.Entities;

[Table("tbd_PrintSessions")]
public partial class PrintSessionEntity
{
    [Key]
    public int SessionId { get; set; }

    public int PrinterId { get; set; }

    public int EmployeeId { get; set; }

    public int PrintJobId { get; set; }

    public int PagesPrinted { get; set; }

    public int StatusId { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("PrintSessions")]
    public virtual EmployeeEntity Employee { get; set; } = null!;

    [ForeignKey("PrintJobId")]
    [InverseProperty("PrintSessions")]
    public virtual PrintJobNameEntity PrintJob { get; set; } = null!;

    [ForeignKey("PrinterId")]
    [InverseProperty("PrintSessions")]
    public virtual PrinterEntity Printer { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("PrintSessions")]
    public virtual StatusEntity Status { get; set; } = null!;
}