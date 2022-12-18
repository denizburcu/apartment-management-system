using ApartmentManagement.Core.Contracts;
using ApartmentManagement.Core.IUnitOfWorks;
using ApartmentManagement.Core.Models;

namespace ApartmentManagement.Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public MessageService(IMessageRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddMessage(Message message)
        {
            await _repository.AddAsync(message);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            var messages = await _repository.GetAllIncludeUser();
            return messages;
        }

        public async Task UpdateNewMessageStatus()
        {
            var messages = await _repository.GetAll();
            foreach (var message in messages)
            {
                if (message.Status == MessageStatus.NEW)
                {
                    message.Status = MessageStatus.NOT_READ;
                    _repository.Update(message);
                }
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task<Message?> GetById(int id)
        {
            var message = await _repository.GetByIdIncludeUser(id);
            if (message != null)
            {
                message.Status = MessageStatus.READ;
                _repository.Update(message);
                await _unitOfWork.CommitAsync();
            }
            return message;
        }

        public async Task<IEnumerable<Message>> GetAllMessagesByUser(string userId)
        {
            return await _repository.GetAllByUserIdAndIncludeUserAsync(userId);
        }
    }
}
