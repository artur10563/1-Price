using E_SHOP.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace E_SHOP.UI.Models.PostDTOs
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
		public Currency Currency { get; set; }

		public string? ImgPath { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
