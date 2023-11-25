using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnePrice.Domain.Entities.ChatEntities;

namespace OnePrice.Infrastructure.Data.EntityTypeConfiguration
{
	internal class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
	{
		public void Configure(EntityTypeBuilder<Message> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnAdd();

			builder.Property(x => x.LastModifiedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnUpdate()
				.IsRequired(false);

			builder.Property(x => x.Content)
				.HasMaxLength(4096)
				.IsRequired(true);

			builder.HasOne(x=>x.Author)
				.WithMany(a=>a.Messages)
				.HasForeignKey(a=>a.AuthorId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
