using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using E_SHOP.Domain.Entities;

namespace E_SHOP.Infrastructure.Data.EntityTypeConfiguration
{
	internal class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
	{
		public void Configure(EntityTypeBuilder<Tag> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValue(DateTime.UtcNow)
				.ValueGeneratedOnAdd();

			builder.Property(x => x.LastModifiedAt)
				.HasDefaultValue(DateTime.UtcNow)
				.ValueGeneratedOnUpdate()
				.IsRequired(false);

			builder.Property(x => x.Name)
				.HasMaxLength(50)
				//.HasAnnotation("MinLength", new MinLengthAttribute(3))
				.IsRequired();

		}
	}
}
