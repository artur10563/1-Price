namespace OnePrice.UI.Models.ChatDTOs
{
	public class ChatMessageDTO
	{
		public string Content { get; set; }
		public ChatUserDTO Author { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
