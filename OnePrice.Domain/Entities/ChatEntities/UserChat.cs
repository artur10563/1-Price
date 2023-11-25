namespace OnePrice.Domain.Entities.ChatEntities
{
	public class UserChat
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ChatId { get; set; }
		public virtual AppUser User { get; set; }
		public virtual Chat Chat { get; set; }

	}
}
