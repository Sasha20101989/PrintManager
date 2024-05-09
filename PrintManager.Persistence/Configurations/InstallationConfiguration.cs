using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Configurations;

public partial class InstallationConfiguration : IEntityTypeConfiguration<InstallationEntity>
{
    public void Configure(EntityTypeBuilder<InstallationEntity> entity)
    {
        entity.HasKey(e => e.InstallationId)
            .HasName("PK__tbd_Inst__5F69B614F3601FB9");

        entity.Property(e => e.InstallationId)
            .ValueGeneratedNever();

        entity.HasOne(d => d.Branch)
            .WithMany(p => p.Installations)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_BranchID");

        entity.HasOne(d => d.Printer)
            .WithMany(p => p.Installations)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PrinterID");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<InstallationEntity> entity);
}
