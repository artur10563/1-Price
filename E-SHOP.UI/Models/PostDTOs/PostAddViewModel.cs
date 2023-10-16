namespace E_SHOP.UI.Models.PostDTOs
{

	public class PostAddViewModel
	{
		public PostAddDTO Post { get; set; }
		public ICollection<TagDTO>? AvailableTags { get; set; }
		public ICollection<CategoryDTO>? AvailableCategories { get; set; }
	}
}
