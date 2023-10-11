using System.ComponentModel.DataAnnotations;

namespace E_SHOP.UI.Models.Home
{
	public class HomeCategoryDTO
	{
		[Required(ErrorMessage = "Name required")]
		[StringLength(50, MinimumLength = 3)]
		public string Name { get; set; }
		public string? ImgPath { get; set; }
	}
}
