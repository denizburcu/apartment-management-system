using ApartmentManagement.Core.Models;

namespace ApartmentManagement.Core.Services
{
    public interface IMessageService
    {
        Task AddMessage(Message message);
        Task<IEnumerable<Message>> GetAllMessages();
        Task<IEnumerable<Message>> GetAllMessagesByUser(string userId);
        Task<Message?> GetById(int id);
        Task UpdateNewMessageStatus();


    }
}
