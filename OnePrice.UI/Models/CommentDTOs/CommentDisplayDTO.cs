using System.ComponentModel.DataAnnotations;
using OnePrice.UI.Models.CommonDTOs;

namespace OnePrice.UI.Models.CommentDTOs
{
	public class CommentDisplayDTO
	{
		public DateTime CreatedAt { get; set; }

		[Required]
		public CommentAppUserDTO Author { get; set; }

		[Required]
		[StringLength(255, MinimumLength = 20)]
		public string Content { get; set; }

		[Required]
		public int PostId { get; set; }
	}
}
