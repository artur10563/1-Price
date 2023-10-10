using E_SHOP.Domain.Common;
using E_SHOP.Domain.Enums;


namespace E_SHOP.Domain.Entities
{
	public class Post : BaseEntity
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		public Currency Currency { get; set; }

		public bool? IsActive { get; set; }

		public string? ImgPath { get; set; }

		public int? CategoryId { get; set; }
		public virtual Category? Category { get; set; }

		//GUID бо IdentityUser по дефолту має Id як GUID
		//public Guid CreatorId { get; set; }
		//public User {get;set;}

		public virtual ICollection<PostTag> Tags { get; set; }
	}
}
