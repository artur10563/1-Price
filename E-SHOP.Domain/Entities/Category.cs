using E_SHOP.Domain.Common;


namespace E_SHOP.Domain.Entities
{
	public class Category : BaseEntity
	{

		public string Name { get; set; }

		public string? ImgPath { get; set; }

		public virtual ICollection<Post> Posts { get; set; }
	}
}
