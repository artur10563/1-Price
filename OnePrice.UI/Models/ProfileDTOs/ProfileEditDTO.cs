using OnePrice.UI.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace OnePrice.UI.Models.ProfileDTOs
{
	public class ProfileEditDTO
	{
		[Required(ErrorMessage = "Nickname is required")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Nickname must be between 3 and 50 characters")]
		public string Nickname { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Email must be between 3 and 50 characters")]
		public string Email { get; set; }

		[OptionalPhoneNumberValidation(ErrorMessage ="Phone number must be 10-15 symbols long")]
		public string? PhoneNumber { get; set; }
		public string? ImgPath { get; set; }

	}

}