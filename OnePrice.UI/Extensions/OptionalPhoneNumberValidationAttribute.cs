using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OnePrice.UI.Extensions
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class OptionalPhoneNumberValidationAttribute : ValidationAttribute
	{
		private static readonly Regex PhoneNumberRegex = new Regex(@"^\d{10,15}$");

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var phoneNumber = value as string;

			if (!string.IsNullOrEmpty(phoneNumber))
			{
				if (!PhoneNumberRegex.IsMatch(phoneNumber))
				{
					return new ValidationResult(ErrorMessage);
				}
			}

			return ValidationResult.Success;
		}
	}
}
