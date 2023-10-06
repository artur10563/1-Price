using E_SHOP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_SHOP.Infrastructure.EntityTypeConfiguration
{
	internal class TagTypeConfiguration : IEntityTypeConfiguration<Tag>
	{
		public void Configure(EntityTypeBuilder<Tag> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValue(DateTime.UtcNow)
				.ValueGeneratedOnAdd();

			builder.HasMany(x => x.Posts);
		}
	}
}
