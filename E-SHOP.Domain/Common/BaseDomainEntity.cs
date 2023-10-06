using System.ComponentModel.DataAnnotations;


namespace E_SHOP.Domain.Common
{
	public abstract class BaseDomainEntity
	{
		[Key]
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? LastModifiedAt { get; set; }
	}
}
