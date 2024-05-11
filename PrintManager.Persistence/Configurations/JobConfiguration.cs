using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Configurations;

public partial class JobConfiguration : IEntityTypeConfiguration<JobEntity>
{
    public void Configure(EntityTypeBuilder<JobEntity> entity)
    {
        entity.HasKey(e => e.JobId)
            .HasName("PK__tbd_Jobs__C9F492902B921EEB");

        entity.Property(e => e.JobId)
            .ValueGeneratedOnAdd();

        entity.HasOne(d => d.Employee)
            .WithMany(p => p.Jobs)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_EmployeeId");

        entity.HasOne(d => d.Printer)
            .WithMany(p => p.Jobs)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PrinterId_Session");

        entity.HasOne(d => d.Status)
            .WithMany(p => p.Jobs)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_tbd_Jobs_tbd_Statuses");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<JobEntity> entity);
}
