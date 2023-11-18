using OnePrice.UI.Models.CommonIdDTOs;
using OnePrice.UI.Models.PostDTOs;

namespace OnePrice.UI.Models.CommonDTOs
{
	public class SearchDTO : IAvailableDataDTO
	{
		public string Search { get; set; }
		public string Category { get; set; }
		public string Currency { get; set; }

		public int? MinPrice { get; set; }
		public int? MaxPrice { get; set; }

		public string SortOrder { get; set; }
		public string SortField { get; set; }

		public ICollection<CommonIdTagDTO>? AvailableTags { get; set; }
		public ICollection<CommonIdCategoryDTO>? AvailableCategories { get; set; }
		public ICollection<CommonIdCurrencyDTO>? AvailableCurrencies { get; set; }
	}
}
