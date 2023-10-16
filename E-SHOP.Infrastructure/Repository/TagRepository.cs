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
	public class TagRepository : BaseRepository<Tag>, ITagRepository
	{
		public TagRepository(AppDbContext context) : base(context) { }
	}
}
