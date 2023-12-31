﻿using OnePrice.Application.Repository;
using OnePrice.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePrice.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;

        public ITagRepository Tags { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IPostRepository Posts { get; private set; }
        public IAppUserRepository Users { get; private set; }
        public ICurrencyRepository Currencies { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IChatRepository Chats { get; private set; }

        public UnitOfWork(AppDbContext context,
            ITagRepository TagRepository,
            ICategoryRepository CategoryRepository,
            IPostRepository PostRepository,
            IAppUserRepository UserRepository,
            ICurrencyRepository CurrencyRepository,
            ICommentRepository CommentRepository,
            IChatRepository ChatRepository)
        {
            _context = context;
            Tags = TagRepository ?? throw new ArgumentNullException(nameof(TagRepository));
            Categories = CategoryRepository ?? throw new ArgumentNullException(nameof(CategoryRepository));
            Posts = PostRepository ?? throw new ArgumentNullException(nameof(PostRepository));
            Users = UserRepository ?? throw new ArgumentNullException(nameof(UserRepository));
            Currencies = CurrencyRepository ?? throw new ArgumentNullException(nameof(CurrencyRepository));
            Comments = CommentRepository ?? throw new ArgumentNullException(nameof(CommentRepository));
            Chats = ChatRepository ?? throw new ArgumentNullException(nameof(ChatRepository));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
