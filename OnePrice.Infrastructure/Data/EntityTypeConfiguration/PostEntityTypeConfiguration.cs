using OnePrice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;


namespace OnePrice.Infrastructure.Data.EntityTypeConfiguration
{
	internal class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
	{
		public void Configure(EntityTypeBuilder<Post> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnAdd();

			builder.Property(x => x.LastModifiedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnUpdate()
				.IsRequired(false);

			builder.Property(p => p.Title)
				.HasMaxLength(50)
				.IsRequired();

			builder.Property(p => p.Description)
				.HasMaxLength(250)
				.IsRequired();

			builder.Property(p => p.Price)
				.HasColumnType("decimal(18, 2)")
				.IsRequired();

			builder.Property(p => p.Currency)
				.IsRequired();


			builder.Property(p => p.IsActive)
				.HasDefaultValue(true)
				.ValueGeneratedOnAdd();

			builder.Property(p => p.ImgPath)
				.IsRequired(false);


			builder.HasOne(p => p.Category)
				.WithMany(c => c.Posts)
				.HasForeignKey(p => p.CategoryId)
				.OnDelete(DeleteBehavior.NoAction);


			builder.HasOne(p => p.Author)
			.WithMany(u => u.Posts)
			.HasForeignKey(p => p.AuthorId)
			.OnDelete(DeleteBehavior.NoAction);



		}
	}
}
