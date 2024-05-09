using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Configurations;

public partial class BranchConfiguration : IEntityTypeConfiguration<BranchEntity>
{
    public void Configure(EntityTypeBuilder<BranchEntity> entity)
    {
        entity.HasKey(e => e.BranchId)
            .HasName("PK__tbd_Bran__A1682FA51A1DD186");

        entity.Property(e => e.BranchId)
            .ValueGeneratedNever();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<BranchEntity> entity);
}

