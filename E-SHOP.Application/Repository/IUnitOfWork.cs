using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_SHOP.Application.Repository
{
	public interface IUnitOfWork : IDisposable
	{
		ITagRepository Tags { get; }
		ICategoryRepository Categories { get; }
		IPostRepository Posts { get; }

		int SaveChanges();
	}
}
