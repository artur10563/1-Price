using System.ComponentModel.DataAnnotations;

namespace E_SHOP.UI.Models.Post
{

	public class CategoryDTO
	{
		[Key]
		public int Id { get; set; }	

		[Required(ErrorMessage = "Category name is required")]
		[StringLength(50, MinimumLength = 3)]
		public string Name { get; set; }
	}
}
