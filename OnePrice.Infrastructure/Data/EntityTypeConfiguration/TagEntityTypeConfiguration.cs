using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnePrice.Domain.Entities;

namespace OnePrice.Infrastructure.Data.EntityTypeConfiguration
{
	internal class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
	{
		public void Configure(EntityTypeBuilder<Tag> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnAdd();

			builder.Property(x => x.LastModifiedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnUpdate()
				.IsRequired(false);

			builder.Property(x => x.Name)
				.HasMaxLength(50)
				.IsRequired();

		}
	}
}
