using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Configurations
{
    public partial class PrintJobNameConfiguration : IEntityTypeConfiguration<PrintJobNameEntity>
    {
        public void Configure(EntityTypeBuilder<PrintJobNameEntity> entity)
        {
            entity.HasKey(e => e.PrintJobId).HasName("PK__tbd_Prin__18013520BE15931E");

            entity.Property(e => e.PrintJobId).ValueGeneratedNever();

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PrintJobNameEntity> entity);
    }
}
