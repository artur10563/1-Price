using OnePrice.UI.Models.PostDTOs;

namespace OnePrice.UI.Models.ProfileDTOs
{
	public class ProfileIndexDTO
	{
		public string Nickname { get; set; }
		public string? ImgPath { get; set; }

		public ICollection<PostDisplayDTO>? Posts { get; set; }
	}
}
