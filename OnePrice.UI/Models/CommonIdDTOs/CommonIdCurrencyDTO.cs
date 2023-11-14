using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.CommonIdDTOs
{
	public class CommonIdCurrencyDTO
	{
		public int Id { get; set; }
		[Required]
		[RegularExpression("^[A-Z]{3}$", ErrorMessage = "Currency code must be 3 uppercase letters.")]
		public string Code { get; set; }

		[Required]
		[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Currency name must contain only letters.")]
		[StringLength(10, MinimumLength = 3, ErrorMessage = "Currency name must be 3-10 symbols long")]
		public string FullName { get; set; }

		[Required]
		public char Symbol { get; set; }
	}
}
