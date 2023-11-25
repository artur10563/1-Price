
using OnePrice.Application.Repository.Common;
using OnePrice.Domain.Entities.ChatEntities;

namespace OnePrice.Application.Repository
{
    public interface IChatRepository : IBaseRepository<Chat>
    {
        Task<Chat?> GetByUserIdsAsync(int userId1, int userId2);
        Task<Chat?> GetFullByIdAsync(int id);
    }
}
