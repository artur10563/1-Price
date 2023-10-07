using E_SHOP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_SHOP.Infrastructure.Data.EntityTypeConfiguration
{
	internal class PostCategoryEntityTypeConfiguration : IEntityTypeConfiguration<PostCategory>
	{
		public void Configure(EntityTypeBuilder<PostCategory> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.Category)
				.WithMany(x => x.Posts)
				.HasForeignKey(x => x.CategoryId)
				.OnDelete(DeleteBehavior.NoAction);

			//Помилка
			//builder.HasOne(x => x.Post)
			//	.WithOne(x => x.Category);

		}
	}
}
