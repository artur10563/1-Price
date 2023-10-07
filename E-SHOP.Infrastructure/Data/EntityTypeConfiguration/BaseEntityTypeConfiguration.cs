using E_SHOP.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_SHOP.Infrastructure.Data.EntityTypeConfiguration
{
    internal class BaseEntityTypeConfiguration : IEntityTypeConfiguration<BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.LastModifiedAt)
                .HasDefaultValue(DateTime.UtcNow)
                .ValueGeneratedOnUpdate()
                .IsRequired(false);

        }
    }
}
