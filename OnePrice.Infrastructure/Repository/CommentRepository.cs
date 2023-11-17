using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Infrastructure.Data;
using OnePrice.Infrastructure.Repository.Common;

namespace OnePrice.Infrastructure.Repository
{
	public class CommentRepository : BaseRepository<Comment>, ICommentRepository
	{
		public CommentRepository(AppDbContext context) : base(context) { }
	}
}
