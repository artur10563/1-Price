using E_SHOP.Application.Repository.Common;
using E_SHOP.Domain.Entities;

namespace E_SHOP.Application.Repository
{
	public interface IPostRepository : IBaseRepository<Post>
	{
		Task<Post?> GetByIdWithTagsAsync(int id);
		Task<Post?> GetByIdFullAsync(int id);
	}
}
