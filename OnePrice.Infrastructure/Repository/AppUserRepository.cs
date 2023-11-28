using Microsoft.EntityFrameworkCore;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Infrastructure.Data;
using OnePrice.Infrastructure.Repository.Common;


namespace OnePrice.Infrastructure.Repository
{
	public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
	{
		public AppUserRepository(AppDbContext context) : base(context) { }

		public async Task<AppUser>? GetByEmailAsync(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task<AppUser?> GetByEmailWithPostsAsync(string email)
		{
			return
				await
				_context.Users.Include(u => u.Posts)
					.ThenInclude(p => p.Currency)
				.Include(u => u.FavoritePosts)
					.ThenInclude(fp => fp.Post)
						.ThenInclude(p => p.Currency)
				.FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task<AppUser>? GetByEmailWithChatsAsync(string email)
		{
			return
				await
				_context.Users
				.Include(u => u.Chats)
					.ThenInclude(c => c.Chat)
						.ThenInclude(c => c.Members)
							.ThenInclude(m => m.User)
				.FirstOrDefaultAsync(u => u.Email == email);

		}

	}
}
