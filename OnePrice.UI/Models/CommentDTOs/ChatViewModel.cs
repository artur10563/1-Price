using OnePrice.UI.Models.ChatDTOs;

namespace OnePrice.UI.Models.CommentDTOs
{
	public class ChatViewModel
	{
		public ChatDTO Chat { get; set; }
		public ICollection<ChatDTO> AvailableChats { get; set; }
	}
}
