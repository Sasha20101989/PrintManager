using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Configurations;

public partial class StatusConfiguration : IEntityTypeConfiguration<StatusEntity>
{
    public void Configure(EntityTypeBuilder<StatusEntity> entity)
    {
        entity.Property(e => e.StatusName)
            .IsFixedLength();

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<StatusEntity> entity);
}
