
using OnePrice.Domain.Common;

namespace OnePrice.Domain.Entities
{
	public class Complaint : BaseEntity
	{
		public string Content { get; set; }

		public int? AuthorId { get; set; }
		public virtual AppUser? Author { get; set; }

		public int? PostId { get; set; }
		public virtual Post? Post { get; set; }

		public int StatusId { get; set; }
		public virtual ComplaintStatus Status { get; set; }

		public int TypeId { get; set; }
		public virtual ComplaintType Type { get; set; }
	}
}
