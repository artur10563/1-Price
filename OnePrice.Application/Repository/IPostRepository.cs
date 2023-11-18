using OnePrice.Application.Repository.Common;
using OnePrice.Domain.Entities;

namespace OnePrice.Application.Repository
{
	public interface IPostRepository : IBaseRepository<Post>
	{
		Task<Post?> GetByIdWithTagsAsync(int id);
		Task<Post?> GetByIdFullAsync(int id);
		public IEnumerable<Post>? GetFiltered(
			string search = "",
			string category = "",
			string currency = "",
			int? minPrice = null,
			int? maxPrice = null,
			string sortField = "",
			string sortOrder = "asc");
	}
}
