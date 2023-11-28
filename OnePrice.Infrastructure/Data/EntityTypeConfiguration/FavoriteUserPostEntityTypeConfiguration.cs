using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnePrice.Domain.Entities;


namespace OnePrice.Infrastructure.Data.EntityTypeConfiguration
{
	internal class FavoriteUserPostEntityTypeConfiguration : IEntityTypeConfiguration<FavoriteUserPost>
	{
		public void Configure(EntityTypeBuilder<FavoriteUserPost> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasIndex(x => new { x.UserId, x.PostId }).IsUnique(); 

			builder.HasOne(x => x.User)
				.WithMany(x => x.FavoritePosts)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(x => x.Post)
				.WithMany(x => x.FavoritedBy)
				.HasForeignKey(x => x.PostId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
