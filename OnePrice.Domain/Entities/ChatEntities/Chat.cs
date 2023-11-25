using OnePrice.Domain.Common;

namespace OnePrice.Domain.Entities.ChatEntities
{
	public class Chat : BaseEntity
	{
		public string Title { get; set; }
		public virtual ICollection<UserChat> Members { get; set; }
		public virtual ICollection<Message> Messages { get; set; }
	}
}
