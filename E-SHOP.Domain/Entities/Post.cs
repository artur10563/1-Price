using E_SHOP.Domain.Common;
using E_SHOP.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace E_SHOP.Domain.Entities
{
	public class Post : BaseDomainEntity
	{
		[Required(ErrorMessage = "Enter title")]
		[StringLength(50, MinimumLength = 3)]
		public string Title { get; set; }

		[Required(ErrorMessage = "Enter description")]
		[StringLength(255, MinimumLength = 50)]
		public string Description { get; set; }

		[Required(ErrorMessage = "Enter price")]
		[Range(0, double.MaxValue,
			ErrorMessage = "Price must be a non-negative value")]
		public decimal Price { get; set; }

		public Currency Currency { get; set; }

		public bool IsActive { get; set; }

		public string? ImgPath { get; set; }

		public int IdCategory { get; set; }

		public Guid IdCreator { get; set; }

		public List<Tag> Tags { get; set; }
	}
}
