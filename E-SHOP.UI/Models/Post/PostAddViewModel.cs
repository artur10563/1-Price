namespace E_SHOP.UI.Models.Post
{
	public class PostAddViewModel
	{
		public PostDTO Post { get; set; }
		public ICollection<TagDTO> AvailableTags { get; set; }
		public ICollection<CategoryDTO> AvailableCategories { get; set; }
	}
}
