using OnePrice.UI.Models.CommonDTOs;

namespace OnePrice.UI.Models.ChatDTOs
{
	public class ChatDTO
	{
		public int ChatId { get; set; }
		public string Title { get; set; }

		public CommonAppUserDTO Sender { get; set; }
		public CommonAppUserDTO Receiver { get; set; }

		public ICollection<ChatMessageDTO>? Messages { get; set; }
	}
}
