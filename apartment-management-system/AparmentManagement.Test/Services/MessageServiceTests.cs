using ApartmentManagement.Core.Services;
using ApartmentManagement.Core.IUnitOfWorks;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Service.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AparmentManagement.Test.Services
{
    public class MessageServiceTests
    {
        private readonly Mock<IMessageRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly MessageService _messageService;
        private readonly List<Message> _messageList;

        public MessageServiceTests()
        {
            _mockRepo = new Mock<IMessageRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _messageService = new MessageService(_mockRepo.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async void AddMessage_ReturnVoid()
        {
            //arrange
            var message = CreateMessage();
            _mockRepo.Setup(g => g.AddAsync(message)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);
            //act
            await _messageService.AddMessage(message);
            //assert
            _mockRepo.Verify(g => g.AddAsync(message));
            _mockUnitOfWork.Verify(g => g.CommitAsync());
        }

        [Fact]
        public async void GetAllMessages_ShouldReturns_AllMessages()
        {
            //arrange
            var messages = CreateMessageList();
            _mockRepo.Setup(r => r.GetAllIncludeUser()).ReturnsAsync(messages);
            //act
            var result = await _messageService.GetAllMessages();
            //assert
            _mockRepo.Verify(r => r.GetAllIncludeUser());
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void UpdateNewMessageStatus_ShouldUpdatesStatus_ReturnVoid()
        {
            //arrange
            var messages = CreateMessageList();
            var message = CreateMessage();
            _mockRepo.Setup(m => m.GetAll()).ReturnsAsync(messages);
            _mockRepo.Setup(m => m.Update(It.IsAny<Message>()));
            _mockUnitOfWork.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);
            //act
            await _messageService.UpdateNewMessageStatus();
            //assert
            _mockRepo.Verify(m => m.Update(It.IsAny<Message>()), Times.Exactly(messages.Count()));
            Assert.Equal(MessageStatus.NOT_READ, messages[0].Status);
            Assert.Equal(MessageStatus.NOT_READ, messages[1].Status);
        }

        [Fact]
        public async void GetById_ShouldUpdatesStatus_ReturnMessage()
        {
            //arrange
            var message = CreateMessage();

            _mockRepo.Setup(m => m.GetByIdIncludeUser(1)).ReturnsAsync(message);
            _mockRepo.Setup(m => m.Update(It.IsAny<Message>()));
            _mockUnitOfWork.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);
            //act
            var result = await _messageService.GetById(1);
            //assert
            _mockRepo.Verify(m => m.Update(It.IsAny<Message>()), Times.Once);
            Assert.Equal(MessageStatus.READ, result.Status);
        }

        [Fact]
        public async void GetAllMessagesByUser_NotUpdateStatus_GetAllMessages()
        {
            //arrange
            var message = CreateMessageList();
            _mockRepo.Setup(m => m.GetAllByUserIdAndIncludeUserAsync("1")).ReturnsAsync(message);
            //act
            var result = await _messageService.GetAllMessagesByUser("1");
            //assert
            _mockRepo.Verify(m => m.GetAllByUserIdAndIncludeUserAsync("1"), Times.Once);
            Assert.Equal(2, result.Count());
        }


        private Message CreateMessage()
        {
            return new Message()
            {
                Id = 1,
                Description = "Test",
                UserId = "1"
            };
        }

        private List<Message> CreateMessageList()
        {
            return new List<Message>()
            {
                new Message()
                {
                   Id = 1,
                   Description = "Test",
                   Status = MessageStatus.NEW,
                   UserId = "1"
                },
                new Message()
                {
                   Id = 2,
                   Description = "Test",
                   Status = MessageStatus.NEW,
                   UserId = "1"
                },
            };
        }
    }
}
