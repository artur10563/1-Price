namespace OnePrice.Application.Repository
{
	public interface IUnitOfWork : IDisposable
	{
		ITagRepository Tags { get; }
		ICategoryRepository Categories { get; }
		IPostRepository Posts { get; }
		IAppUserRepository Users { get; }
		ICurrencyRepository Currencies { get; }
		ICommentRepository Comments { get; }
		IChatRepository Chats { get; }
		IComplaintStatusRepository ComplaintStatuses { get; }
		IComplaintTypeRepository ComplaintTypes { get; }
		IComplaintRepository Complaints { get; }

		int SaveChanges();
		Task<int> SaveChangesAsync();
	}
}
