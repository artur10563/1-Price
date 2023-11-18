using OnePrice.UI.Models.CommonIdDTOs;

namespace OnePrice.UI.Models.PostDTOs
{
	public interface IAvailableDataDTO
	{
		ICollection<CommonIdTagDTO>? AvailableTags { get; set; }
		ICollection<CommonIdCategoryDTO>? AvailableCategories { get; set; }
		ICollection<CommonIdCurrencyDTO>? AvailableCurrencies { get; set; }
	}
}
