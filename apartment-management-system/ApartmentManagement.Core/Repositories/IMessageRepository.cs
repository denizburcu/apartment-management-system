using ApartmentManagement.Core.Models;
using ApartmentManagement.Core.Repositories;


namespace ApartmentManagement.Core.Services
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        Task<IEnumerable<Message>> GetAllIncludeUser();
        Task<IEnumerable<Message>> GetAllByUserIdAndIncludeUserAsync(string userId);
        Task<Message?> GetByIdIncludeUser(int id);
    }
}
