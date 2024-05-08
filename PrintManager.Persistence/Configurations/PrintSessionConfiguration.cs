using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Configurations
{
    public partial class PrintSessionConfiguration : IEntityTypeConfiguration<PrintSessionEntity>
    {
        public void Configure(EntityTypeBuilder<PrintSessionEntity> entity)
        {
            entity.HasKey(e => e.SessionId).HasName("PK__tbd_Prin__C9F49290E23ECC3C");

            entity.Property(e => e.SessionId).ValueGeneratedNever();

            entity.HasOne(d => d.Employee).WithMany(p => p.PrintSessions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbd_Print__Emplo__49C3F6B7");

            entity.HasOne(d => d.PrintJob).WithMany(p => p.PrintSessions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbd_Print__Print__4BAC3F29");

            entity.HasOne(d => d.Printer).WithMany(p => p.PrintSessions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbd_Print__Print__48CFD27E");

            entity.HasOne(d => d.Status).WithMany(p => p.PrintSessions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbd_Print__Statu__4AB81AF0");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PrintSessionEntity> entity);
    }
}
