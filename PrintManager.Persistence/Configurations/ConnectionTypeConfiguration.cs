using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Configurations
{
    public partial class ConnectionTypeConfiguration : IEntityTypeConfiguration<ConnectionTypeEntity>
    {
        public void Configure(EntityTypeBuilder<ConnectionTypeEntity> entity)
        {
            entity.HasKey(e => e.ConnectionTypeId).HasName("PK__tbd_Conn__69EB7405C3E431C9");

            entity.Property(e => e.ConnectionTypeId).ValueGeneratedNever();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<ConnectionTypeEntity> entity);
    }
}
