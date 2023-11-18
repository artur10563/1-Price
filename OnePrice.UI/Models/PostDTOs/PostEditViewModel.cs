using OnePrice.UI.Models.CommonIdDTOs;


namespace OnePrice.UI.Models.PostDTOs
{
	public class PostEditViewModel : IAvailableDataDTO
	{
		public PostEditDTO Post { get; set; }
		public ICollection<CommonIdTagDTO>? AvailableTags { get; set; }
		public ICollection<CommonIdCategoryDTO>? AvailableCategories { get; set; }
		public ICollection<CommonIdCurrencyDTO>? AvailableCurrencies { get; set; }
	}
}
