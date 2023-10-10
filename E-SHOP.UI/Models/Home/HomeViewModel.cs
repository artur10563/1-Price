using E_SHOP.Domain.Entities;

namespace E_SHOP.UI.Models.Home
{
	public class HomeViewModel
	{
		public List<CategoryViewModel> Categories { get; set; }
		public List<PostViewModel> Posts { get; set; }
	}
}
