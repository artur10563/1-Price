using OnePrice.UI.Models.CommonDTOs;
using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.PostDTOs
{
	public class PostDisplayDTO
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Title required")]
		[StringLength(50, MinimumLength = 3)]
		public string Title { get; set; }


		[Required(ErrorMessage = "Price required")]
		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Currency required")]
		public CommonCurrencyDTO Currency { get; set; }

		public string? ImgPath { get; set; }

		public int Year { get; set; }
		public string Month { get; set; }
		public int Day { get; set; }
	}
}
