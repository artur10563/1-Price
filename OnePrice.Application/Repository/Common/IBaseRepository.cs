using OnePrice.Domain.Common;

namespace OnePrice.Application.Repository.Common
{
	public interface IBaseRepository<T> where T : BaseEntity
	{

		IEnumerable<T>? GetAll();
		IEnumerable<T>? GetAll(Func<T, bool> predicate);

		Task<T?> GetByIdAsync(int id);

		Task AddAsync(T entity);

		Task AddRangeAsync(IEnumerable<T> entities);

		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);

		void Update(T entity);
		T? FirstOrDefault(Func<T, bool> predicate);
	}
}
