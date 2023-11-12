using OnePrice.Domain.Common;


namespace OnePrice.Domain.Entities
{
	public class AppUser : BaseEntity
	{
		public string Nickname { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string? ImgPath { get; set; }

		public ICollection<Post>? Posts { get; set; }
		public ICollection<Comment>? Comments { get; set; }

	}
}

