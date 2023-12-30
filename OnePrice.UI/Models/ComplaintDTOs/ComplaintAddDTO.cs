namespace OnePrice.UI.Models.ComplaintDTOs
{
	public class ComplaintAddDTO
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public int AuthorId { get; set; }
		public int PostId { get; set; }
		public int TypeId { get; set; }
	}
}
