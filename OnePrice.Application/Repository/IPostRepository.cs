using OnePrice.Application.Repository.Common;
using OnePrice.Domain.Entities;

namespace OnePrice.Application.Repository
{
	public interface IPostRepository : IBaseRepository<Post>
	{
		Task<Post?> GetByIdWithTagsAsync(int id);
		Task<Post?> GetByIdFullAsync(int id);
	}
}
