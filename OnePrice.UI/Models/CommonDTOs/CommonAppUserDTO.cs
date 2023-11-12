using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.CommonDTOs
{
	public class CommonAppUserDTO
	{
		[Required(ErrorMessage = "Nickname is required")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Nickname must be between 3 and 50 characters")]
		public string Nickname { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Email must be between 3 and 50 characters")]
		public string Email { get; set; }

		[StringLength(15, MinimumLength = 10, ErrorMessage = "Phone Number must be between 10 and 15 characters")]
		public string PhoneNumber { get; set; }
		public string? ImgPath { get; set; }
	}
}
