using OnePrice.Domain.Common;
using OnePrice.Domain.Entities.ChatEntities;

namespace OnePrice.Domain.Entities
{
	public class AppUser : BaseEntity
	{
		public string Nickname { get; set; }
		public string Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? ImgPath { get; set; }

		public virtual ICollection<Post>? Posts { get; set; }
		public virtual ICollection<Comment>? Comments { get; set; }
		public virtual ICollection<UserChat>? Chats { get; set; }
		public virtual ICollection<Message>? Messages { get; set; }
		public virtual ICollection<FavoriteUserPost>? FavoritePosts { get; set; }
	}
}

