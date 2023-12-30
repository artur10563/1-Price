using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Infrastructure.Data;
using OnePrice.Infrastructure.Repository.Common;


namespace OnePrice.Infrastructure.Repository
{
	public class ComplaintTypeRepository : BaseRepository<ComplaintType>, IComplaintTypeRepository
	{
		public ComplaintTypeRepository(AppDbContext context) : base(context) { }
	}
}
