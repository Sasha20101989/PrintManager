using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Configurations
{
    public partial class InstallationConfiguration : IEntityTypeConfiguration<InstallationEntity>
    {
        public void Configure(EntityTypeBuilder<InstallationEntity> entity)
        {
            entity.HasKey(e => e.InstallationId).HasName("PK__tbd_Inst__5F69B6F4F4D4DA66");

            entity.Property(e => e.InstallationId).ValueGeneratedNever();

            entity.HasOne(d => d.Branch).WithMany(p => p.Installations)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbd_Insta__Branc__3F466844");

            entity.HasOne(d => d.Printer).WithMany(p => p.Installations)
                .HasForeignKey(d => d.PrinterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbd_Insta__Print__403A8C7D");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<InstallationEntity> entity);
    }
}
