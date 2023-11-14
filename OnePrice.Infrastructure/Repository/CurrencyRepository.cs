using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Infrastructure.Data;
using OnePrice.Infrastructure.Repository.Common;

namespace OnePrice.Infrastructure.Repository
{
	public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
	{
		public CurrencyRepository(AppDbContext context) : base(context) { }
	}
}
