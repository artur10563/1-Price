namespace OnePrice.Domain.Entities
{
	public class FavoriteUserPost
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int PostId { get; set; }

		public virtual Post Post { get; set; }
		public virtual AppUser User { get; set; }
	}
}
