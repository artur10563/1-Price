
namespace E_SHOP.Domain.Entities
{
	public class PostCategory
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public int PostId { get; set; }

		public virtual Category Category { get; set; }
		public virtual Post Post { get; set; }
	}
}
