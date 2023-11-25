using OnePrice.Application.Repository.Common;
using OnePrice.Domain.Entities;

namespace OnePrice.Application.Repository
{
	public interface IAppUserRepository : IBaseRepository<AppUser>
	{
		Task<AppUser>? GetByEmailAsync(string email);
		Task<AppUser?> GetByEmailWithPostsAsync(string email);
		Task<AppUser?> GetByEmailWithChatsAsync(string email);
	}
}
