using E_SHOP.Domain.Entities;

namespace E_SHOP.UI.Models.Home
{
	public class HomeViewModel
	{
		public List<HomeCategoryDTO> Categories { get; set; }
		public List<HomePostDTO> Posts { get; set; }
	}
}
