using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Infrastructure.Data;
using OnePrice.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;


namespace OnePrice.Infrastructure.Repository
{
	public class PostRepository : BaseRepository<Post>, IPostRepository
	{
		public PostRepository(AppDbContext context) : base(context) { }
		public async Task<Post?> GetByIdCommentsTags(int id)
		{
			return await _context.Posts
				.Include(p => p.Author)
				.Include(p => p.Comments)
				.Include(p => p.Tags)
					.ThenInclude(pt => pt.Tag)
					.FirstOrDefaultAsync(p => p.Id == id);
		}
		public async Task<Post?> GetByIdFullAsync(int id)
		{
			return await _context.Posts
				.Include(p => p.Currency)
				.Include(post => post.Tags)
					.ThenInclude(post => post.Tag)
				.Include(post => post.Category)
				.Include(post => post.Comments
					.OrderByDescending(c => c.CreatedAt))
					.ThenInclude(c => c.Author)
				.Include(post => post.Author)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<Post?> GetByIdWithTagsAsync(int id)
		{
			return await _context.Posts
				.Include(post => post.Currency)
				.Include(post => post.Tags)
				.Include(post => post.Author)
				.FirstOrDefaultAsync(p => p.Id == id);
		}

		public IEnumerable<Post>? GetFiltered(
			string search = "",
			string category = "",
			string currency = "",
			int? minPrice = null,
			int? maxPrice = null,
			string sortField = "",
			string sortOrder = "asc")
		{
			var query = _context.Posts.Where(p => p.IsActive);

			if (!string.IsNullOrEmpty(category))
				query = query.Where(p => p.Category.Name.ToLower() == category.ToLower());


			if (!string.IsNullOrWhiteSpace(search))
			{
				search = search.ToLower();
				query = query.Where(p =>
					p.Title.ToLower().Contains(search) ||
					p.Description.ToLower().Contains(search) ||
					p.Id.ToString() == search);
			}

			if (!string.IsNullOrEmpty(currency))
				query = query.Where(p => p.Currency.FullName.ToLower() == currency.ToLower());


			if (minPrice != null || maxPrice != null)
			{
				if (minPrice != null)
				{
					query = query.Where(p => p.Price >= minPrice);
				}

				if (maxPrice != null)
				{
					query = query.Where(p => p.Price <= maxPrice);
				}
			}

			//Sorting
			if (!string.IsNullOrEmpty(sortField))
			{
				sortOrder = sortOrder == null ? "asc" : sortOrder;
				switch (sortField.ToLower())
				{
					case "createdat":
						query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt);
						break;
					case "price":
						query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
						break;
					case "name":
						query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Title) : query.OrderBy(p => p.Title);
						break;
					case "currency":
						query = sortOrder.ToLower() == "desc" ? query.OrderByDescending(p => p.Currency) : query.OrderBy(p => p.Currency.FullName);
						break;
				}
			}
			query = query.Include(p => p.FavoritedBy).ThenInclude(f => f.User);
			return query;

		}





	}
}
