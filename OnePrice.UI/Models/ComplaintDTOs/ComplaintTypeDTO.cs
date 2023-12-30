using System.ComponentModel.DataAnnotations;

namespace OnePrice.UI.Models.ComplaintDTOs
{
	public class ComplaintTypeDTO
	{
		public int Id { get; set; }
		[Required]
		[StringLength(50, MinimumLength = 3)]
		public string Name { get; set; }
	}
}
