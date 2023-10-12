﻿using E_SHOP.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace E_SHOP.Infrastructure.Data
{
	public class AppDbContext : DbContext //IdentityDbContext<CustomUser>
	{
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Post> Posts { get; set; }

		//public DbSet<CustomUser> Users {get;set;}


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