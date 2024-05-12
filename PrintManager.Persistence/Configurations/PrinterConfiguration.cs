using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Logic.Enums;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Configurations;

public partial class PrinterConfiguration : IEntityTypeConfiguration<PrinterEntity>
{
    public void Configure(EntityTypeBuilder<PrinterEntity> entity)
    {
        entity.HasKey(e => e.PrinterId)
            .HasName("PK__tmp_ms_x__D452AB2148AEC640");

        entity.Property(e => e.PrinterId)
            .ValueGeneratedNever();

        entity.HasOne(d => d.ConnectionType)
            .WithMany(p => p.Printers)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_tbd_Printers_tbd_ConnectionTypes");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<PrinterEntity> entity);
}
