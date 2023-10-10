using E_SHOP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace E_SHOP.Infrastructure.Data.EntityTypeConfiguration
{
	internal class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
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


			builder.Property(c => c.ImgPath)
				   .IsRequired(false);

			builder.HasMany(c => c.Posts)
				.WithOne(c => c.Category)
				.OnDelete(DeleteBehavior.SetNull); 

		}
	}
}
