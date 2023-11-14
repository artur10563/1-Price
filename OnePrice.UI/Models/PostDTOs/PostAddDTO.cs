using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.PostDTOs
{

	public class PostAddDTO
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

		
		public string? ImgPath { get; set; }

		[Required(ErrorMessage = "Currency required")]
		public int CurrencyId { get; set; }

		[Required(ErrorMessage = "Category required")]
		public int CategoryId { get; set; }
		
		[Required(ErrorMessage = "Select atleast one tag")]
		public virtual ICollection<int> TagsId { get; set; }


	}
}
