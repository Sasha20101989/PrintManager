using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Configurations
{
    public partial class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> entity)
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__tbd_Empl__7AD04F11FB34F84E");

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<EmployeeEntity> entity);
    }
}
