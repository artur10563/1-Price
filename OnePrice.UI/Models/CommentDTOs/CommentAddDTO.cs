using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.CommentDTOs
{
	public class CommentAddDTO
	{
		[Required]
		[StringLength(255, MinimumLength = 20)]
		public string Content { get; set; }

		[Required]
		public int PostId { get; set; }
	}
}
