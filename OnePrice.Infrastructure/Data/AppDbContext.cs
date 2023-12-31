﻿using OnePrice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using OnePrice.Domain.Entities.ChatEntities;

namespace OnePrice.Infrastructure.Data
{
    public class AppDbContext : DbContext //IdentityDbContext<CustomUser>
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Chat> Chats { get; set; }

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
