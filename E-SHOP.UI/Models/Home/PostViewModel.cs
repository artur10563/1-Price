using E_SHOP.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace E_SHOP.UI.Models.Home
{
	public class PostViewModel
	{
		[Required(ErrorMessage = "Title required")]
		[StringLength(50, MinimumLength = 3)]
		public string Title { get; set; }

		[Required(ErrorMessage = "Description required")]
		[StringLength(250, MinimumLength = 50)]
		public string Description { get; set; }

		[Required(ErrorMessage = "Price required")]
		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Currency required")]
		public Currency Currency { get; set; }

		public string? ImgPath { get; set; }
	}
}