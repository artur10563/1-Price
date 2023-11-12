using Microsoft.EntityFrameworkCore;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Infrastructure.Data;
using OnePrice.Infrastructure.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePrice.Infrastructure.Repository
{
	public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
	{
		public AppUserRepository(AppDbContext context) : base(context) { }

		public Task<AppUser>? GetByEmailAsync(string email)
		{
			return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}
	}
}
