using OnePrice.Domain.Common;

namespace OnePrice.Domain.Entities.ChatEntities
{
	public class Message : BaseEntity
	{
		public string Content { get; set; }
		public int AuthorId { get; set; }
		public int ChatId { get; set; }
		public virtual AppUser Author { get; set; }
		public virtual Chat Chat { get; set; }

	}
}
