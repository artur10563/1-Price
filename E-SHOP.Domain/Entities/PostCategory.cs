
namespace E_SHOP.Domain.Entities
{
	public class PostCategory
	{
		public int Id { get; set; }
		public int IdCategory { get; set; }
		public int IdPost { get; set; }

		public virtual Category Category { get; set; }
		public virtual Post Post { get; set; }
	}
}
