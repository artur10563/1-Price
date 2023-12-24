using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Infrastructure.Data;
using OnePrice.Infrastructure.Repository.Common;


namespace OnePrice.Infrastructure.Repository
{
	public class ComplaintRepository : BaseRepository<Complaint>, IComplaintRepository
	{
		public ComplaintRepository(AppDbContext context) : base(context) { }
	}
}
