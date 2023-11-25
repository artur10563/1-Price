namespace OnePrice.UI.Models.ChatDTOs
{
	public class ChatDTO
	{
		public int ChatId { get; set; }
		public string Title { get; set; }

		public ICollection<ChatMessageDTO>? Messages { get; set; }
	}
}
