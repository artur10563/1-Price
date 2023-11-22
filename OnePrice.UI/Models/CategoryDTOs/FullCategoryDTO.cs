using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.CategoryDTOs
{
	public class FullCategoryDTO
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name required")]
		[StringLength(50, MinimumLength = 3)]
		public string Name { get; set; }
		public string? ImgPath { get; set; }
	}
}
