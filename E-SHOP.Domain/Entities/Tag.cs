using E_SHOP.Domain.Common;


namespace E_SHOP.Domain.Entities
{
	public class Tag : BaseEntity
	{
		public string Name { get; set; }
		public ICollection<PostTag> Posts { get; set; }
	}
}
