using OnePrice.Application.Repository.Common;
using OnePrice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePrice.Application.Repository
{
	public interface IAppUserRepository : IBaseRepository<AppUser>
	{
		Task<AppUser>? GetByEmailAsync(string email);
	}
}
