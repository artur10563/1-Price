using E_SHOP.Application.Repository;
using E_SHOP.Domain.Entities;
using E_SHOP.Infrastructure.Data;
using E_SHOP.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_SHOP.Infrastructure.Repository
{
	public class PostRepository : BaseRepository<Post>, IPostRepository
	{
		public PostRepository(AppDbContext context) : base(context) { }

		public async Task<Post?> GetByIdFullAsync(int id)
		{
			return await _context.Posts
				.Include(post => post.Tags)
					.ThenInclude(post => post.Tag)
				.Include(post=>post.Category)
				//.Include(post=>post.Comments)
				//.Include(post=>post.User)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<Post?> GetByIdWithTagsAsync(int id)
		{
			return await _context.Posts
				.Include(post => post.Tags)
				.FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
