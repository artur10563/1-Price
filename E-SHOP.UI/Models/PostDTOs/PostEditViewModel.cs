using E_SHOP.UI.Models.CommonIdDTOs;


namespace E_SHOP.UI.Models.PostDTOs
{
	public class PostEditViewModel
	{
		public PostEditDTO Post { get; set; }
		public ICollection<CommonIdTagDTO>? AvailableTags { get; set; }
		public ICollection<CommonIdCategoryDTO>? AvailableCategories { get; set; }
	}
}
