using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Configurations
{
    public partial class StatusConfiguration : IEntityTypeConfiguration<StatusEntity>
    {
        public void Configure(EntityTypeBuilder<StatusEntity> entity)
        {
            entity.HasKey(e => e.StatusId).HasName("PK__tbd_Stat__C8EE20634F9359A6");

            entity.Property(e => e.StatusId).ValueGeneratedNever();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StatusEntity> entity);
    }
}
