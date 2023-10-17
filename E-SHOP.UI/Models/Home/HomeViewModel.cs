using E_SHOP.UI.Models.CommonDTOs;

namespace E_SHOP.UI.Models.Home
{
    public class HomeViewModel
	{
		public List<CommonCategoryDTO> Categories { get; set; }
		public List<HomePostDTO> Posts { get; set; }
	}
}
