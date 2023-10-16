using E_SHOP.Application.Repository.Common;
using E_SHOP.Domain.Common;
using E_SHOP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_SHOP.Infrastructure.Repository.Common
{
	abstract public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
	{
		protected readonly AppDbContext _context;
		protected BaseRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
		}

		public async Task AddRangeAsync(IEnumerable<T> entities)
		{
			await _context.Set<T>().AddRangeAsync(entities);
		}

		public void Remove(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			_context.Set<T>().RemoveRange(entities);
		}

		public async Task<IEnumerable<T>?> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<IEnumerable<T>?> GetAllAsync(Func<T, bool> predicate)
		{
			return await _context.Set<T>()
								   .Where(predicate)
								   .AsQueryable()
								   .ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _context.Set<T>()
				.FirstOrDefaultAsync(e => e.Id == id);
		}
		public T? FirstOrDefault(Func<T, bool> predicate)
		{
			return _context.Set<T>()
				.Where(predicate)
				.FirstOrDefault();
		}

		public void Update(T entity)
		{
			_context.Set<T>().Update(entity);
		}
	}
}
