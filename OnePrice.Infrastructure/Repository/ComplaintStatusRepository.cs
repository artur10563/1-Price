
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Infrastructure.Data;
using OnePrice.Infrastructure.Repository.Common;

namespace OnePrice.Infrastructure.Repository
{
	public class ComplaintStatusRepository : BaseRepository<ComplaintStatus>, IComplaintStatusRepository
	{
		public ComplaintStatusRepository(AppDbContext context) : base(context) { }
	}
}
