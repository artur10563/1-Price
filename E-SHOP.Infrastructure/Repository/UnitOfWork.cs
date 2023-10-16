using E_SHOP.Application.Repository;
using E_SHOP.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_SHOP.Infrastructure.Repository
{
	public class UnitOfWork : IUnitOfWork
	{

		private readonly AppDbContext _context;

		public ITagRepository Tags { get; private set; }

		public ICategoryRepository Categories { get; private set; }

		public IPostRepository Posts { get; private set; }


		public UnitOfWork(AppDbContext context,
			ITagRepository TagRepo,
			ICategoryRepository CategoryRepo,
			IPostRepository PostRepository)
		{
			_context = context;
			Tags = TagRepo ?? throw new ArgumentNullException(nameof(TagRepo));
			Categories = CategoryRepo ?? throw new ArgumentNullException(nameof(CategoryRepo));
			Posts = PostRepository ?? throw new ArgumentNullException(nameof(PostRepository));
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		public int SaveChanges()
		{
			return _context.SaveChanges();
		}
	}
}
