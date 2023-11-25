using Microsoft.EntityFrameworkCore;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities.ChatEntities;
using OnePrice.Infrastructure.Data;
using OnePrice.Infrastructure.Repository.Common;


namespace OnePrice.Infrastructure.Repository
{
	public class ChatRepository : BaseRepository<Chat>, IChatRepository
	{
		public ChatRepository(AppDbContext context) : base(context) { }

		public async Task<Chat?> GetByUserIdsAsync(int userId1, int userId2)
		{
			return await _context.Chats
				.Include(c => c.Members)
				.FirstOrDefaultAsync(c =>
					c.Members.Any(uc => uc.UserId == userId1) &&
					c.Members.Any(uc => uc.UserId == userId2));

		}

		public async Task<Chat?> GetFullByIdAsync(int id)
		{
			return await _context.Chats
				.Include(c => c.Members)
					.ThenInclude(m => m.User)
				.Include(c => c.Messages)
					.ThenInclude(m => m.Author)
				.FirstOrDefaultAsync(c => c.Id == id);

		}
	}
}
