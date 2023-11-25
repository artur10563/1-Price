using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnePrice.Domain.Entities.ChatEntities;

namespace OnePrice.Infrastructure.Data.EntityTypeConfiguration
{
	internal class UserChatEntityTypeConfiguration : IEntityTypeConfiguration<UserChat>
	{
		public void Configure(EntityTypeBuilder<UserChat> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(uc => uc.User)
				.WithMany(u => u.Chats)
				.HasForeignKey(uc => uc.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(uc => uc.Chat)
				.WithMany(c => c.Members)
				.HasForeignKey(uc => uc.ChatId)
				.OnDelete(DeleteBehavior.NoAction);

		}
	}
}
