using E_SHOP.Domain.Common;

namespace E_SHOP.Application.Repository.Common
{
	public interface IBaseRepository<T> where T : BaseEntity
	{

		Task<IEnumerable<T>?> GetAllAsync();
		Task<IEnumerable<T>?> GetAllAsync(Func<T, bool> predicate);

		Task<T?> GetByIdAsync(int id);

		Task AddAsync(T entity);

		Task AddRangeAsync(IEnumerable<T> entities);

		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);

		void Update(T entity);
		T? FirstOrDefault(Func<T, bool> predicate);
	}
}
