using OnePrice.UI.Models.CommonIdDTOs;
using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.Home
{
	public class HomePostDTO
	{
		public int Id { get; set; }
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
		public CommonIdCurrencyDTO Currency { get; set; }

		public string? ImgPath { get; set; }
	}
}