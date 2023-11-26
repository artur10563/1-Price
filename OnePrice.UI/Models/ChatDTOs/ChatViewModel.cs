namespace OnePrice.UI.Models.ChatDTOs
{
    public class ChatViewModel
    {
        public ChatDTO Chat { get; set; }
        public ICollection<ChatDTO> AvailableChats { get; set; }
    }
}
