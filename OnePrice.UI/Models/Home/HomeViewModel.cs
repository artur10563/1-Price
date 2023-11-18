using OnePrice.UI.Models.CommonDTOs;

namespace OnePrice.UI.Models.Home
{
	public class HomeViewModel
	{
		public List<CommonCategoryDTO> Categories { get; set; }
		public List<HomePostDTO> Posts { get; set; }

		public SearchDTO Filters { get; set; }
	}
}
