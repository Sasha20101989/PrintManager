using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Persistence.Entities;
using System.Data;

namespace PrintManager.Persistence.Configurations
{
    public partial class PrinterConfiguration : IEntityTypeConfiguration<PrinterEntity>
    {
        public void Configure(EntityTypeBuilder<PrinterEntity> entity)
        {
            entity.HasKey(e => e.PrinterId).HasName("PK__tbd_Prin__D452AAC1AC74877C");

            entity.Property(e => e.PrinterId).ValueGeneratedNever();

            entity.HasOne(d => d.Branch).WithMany(p => p.Printers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbd_Print__Branc__3B75D760");

            entity.HasOne(d => d.ConnectionType).WithMany(p => p.Printers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbd_Print__Conne__3C69FB99");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PrinterEntity> entity);
    }
}
