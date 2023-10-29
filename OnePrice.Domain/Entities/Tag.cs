using OnePrice.Domain.Common;


namespace OnePrice.Domain.Entities
{
	public class Tag : BaseEntity
	{
		public string Name { get; set; }
		public ICollection<PostTag> Posts { get; set; }
	}
}
