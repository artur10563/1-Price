using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Infrastructure.Data;
using OnePrice.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePrice.Infrastructure.Repository
{
	public class PostRepository : BaseRepository<Post>, IPostRepository
	{
		public PostRepository(AppDbContext context) : base(context) { }

		public async Task<Post?> GetByIdFullAsync(int id)
		{
			return await _context.Posts
				.Include(p => p.Currency)
				.Include(post => post.Tags)
					.ThenInclude(post => post.Tag)
				.Include(post => post.Category)
				.Include(post => post.Comments
					.OrderByDescending(c => c.CreatedAt))
				.Include(post => post.Author)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<Post?> GetByIdWithTagsAsync(int id)
		{
			return await _context.Posts
				.Include(post => post.Currency)
				.Include(post => post.Tags)
				.FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
