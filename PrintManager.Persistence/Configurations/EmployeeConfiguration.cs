using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Configurations;

public partial class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeEntity> entity)
    {
        entity.HasKey(e => e.EmployeeId)
            .HasName("PK__tbd_Empl__7AD04FF13F486E80");

        entity.Property(e => e.EmployeeId)
            .ValueGeneratedNever();

        entity.HasOne(d => d.Branch)
            .WithMany(p => p.Employees)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_BranchID_Emp");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<EmployeeEntity> entity);
}
