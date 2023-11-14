﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePrice.Application.Repository
{
	public interface IUnitOfWork : IDisposable
	{
		ITagRepository Tags { get; }
		ICategoryRepository Categories { get; }
		IPostRepository Posts { get; }
		IAppUserRepository Users { get; }
		ICurrencyRepository Currencies { get; }

		int SaveChanges();
		Task<int> SaveChangesAsync();
	}
}
