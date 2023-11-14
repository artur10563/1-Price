using OnePrice.UI.Models.CommonIdDTOs;

namespace OnePrice.UI.Models.PostDTOs
{
	public interface IPostViewModel
	{
		ICollection<CommonIdTagDTO>? AvailableTags { get; set; }
		ICollection<CommonIdCategoryDTO>? AvailableCategories { get; set; }
		ICollection<CommonIdCurrencyDTO>? AvalaibleCurrencies { get; set; }
	}
}
