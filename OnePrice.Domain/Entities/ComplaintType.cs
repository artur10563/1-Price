
using OnePrice.Domain.Common;

namespace OnePrice.Domain.Entities
{
	public class ComplaintType : BaseEntity
	{
		public string Name { get; set; }
		public virtual ICollection<Complaint> Complaints { get; set; }
	}
}
