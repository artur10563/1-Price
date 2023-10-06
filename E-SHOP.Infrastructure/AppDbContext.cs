using E_SHOP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_SHOP.Infrastructure
{
	internal class AppDbContext : DbContext //IdentityDbContext<CustomUser>
	{
		DbSet<Tag> Tags { get; set; }
		DbSet<Category> Categories { get; set; }
		DbSet<Post> Posts { get; set; }

		//DbSet<CustomUser> Users {get;set;}


		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
			base.OnModelCreating(builder);
		}

	}
}
