using E_SHOP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;


namespace E_SHOP.Infrastructure.Data.EntityTypeConfiguration
{
	internal class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
	{
		public void Configure(EntityTypeBuilder<Post> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValue(DateTime.UtcNow)
				.ValueGeneratedOnAdd();

			builder.Property(x => x.LastModifiedAt)
				.HasDefaultValue(DateTime.UtcNow)
				.ValueGeneratedOnUpdate()
				.IsRequired(false);

			builder.Property(p => p.Title)
				.HasMaxLength(50)
				//.HasAnnotation("MinLength", new MinLengthAttribute(3))
				.IsRequired();

			builder.Property(p => p.Description)
				.HasMaxLength(250)
				//.HasAnnotation("MinLength", new MinLengthAttribute(50))
				.IsRequired();

			builder.Property(p => p.Price)
				 //.HasAnnotation("Range", new RangeAttribute(0, double.MaxValue)
				 //{ ErrorMessage = "Price must be a non-negative value" })
				.HasColumnType("decimal(18, 2)")
				.IsRequired();

			builder.Property(p => p.Currency)
				.IsRequired();



			builder.Property(p => p.IsActive)
				.HasDefaultValue(true)
				.ValueGeneratedOnAdd();

			builder.Property(p => p.ImgPath)
				.IsRequired(false);


			/*
			 * builder.HasOne(p=>p.User)
			 *	.WithMany(u=>u.Posts)
			 *	.HasForeignKey(p=>p.IdCreator);
			 */
		}
	}
}
