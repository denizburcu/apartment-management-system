using ApartmentManagement.Core.Services;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace AparmentManagement.Test.Controllers
{
    public class MessageControllerTests
    {
        private readonly Mock<IMessageService> _mockService;

        private readonly MessageController _controller;

        public MessageControllerTests()
        {
            _mockService = new Mock<IMessageService>();
            _controller = new MessageController(_mockService.Object);
            
        }

        [Fact]
        public async void Index_ActionExecutesAdminUser_ReturnViews()
        {
            //arrange
            CreateAdminUser();
            _mockService.Setup(m => m.GetAllMessages());
            _mockService.Setup(m => m.UpdateNewMessageStatus()).Returns(Task.CompletedTask);
            _mockService.Setup(m => m.GetAllMessagesByUser("1234"));
            //act
            var result = await _controller.Index();
            //asert
            Assert.IsType<ViewResult>(result);
            _mockService.Verify(m => m.GetAllMessagesByUser("1234"), Times.Never);
        }

        [Fact]
        public async void Index_ActionExecutesUser_ReturnViews()
        {
            //arrange
            CreatenUser();
            var userId = "";
            _mockService.Setup(m => m.GetAllMessages());
            _mockService.Setup(m => m.UpdateNewMessageStatus()).Returns(Task.CompletedTask);
            _mockService.Setup(m => m.GetAllMessagesByUser(It.IsAny<string>()));
            //act
            var result = await _controller.Index();
            //asert
            Assert.IsType<ViewResult>(result);
            _mockService.Verify(m => m.UpdateNewMessageStatus(), Times.Never);
            _mockService.Verify(m => m.GetAllMessages(), Times.Never);
            _mockService.Verify(m => m.GetAllMessagesByUser("1234"));

        }

        [Fact]
        public async void Details_Action_ReturnView()
        {
            //arrange
            CreatenUser();
            var message = new Message()
            {
                Id = 1,
                Description = "test",
                Status = MessageStatus.NEW
            };
            _mockService.Setup(m => m.GetById(1)).ReturnsAsync(message);
            //act
            var result = await _controller.Details(1);
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var actualMessage = Assert.IsAssignableFrom<Message>(viewResult.Model);
            Assert.Equal(message.Id, actualMessage.Id);
            _mockService.Verify(m => m.GetById(1), Times.Once);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnView()
        {
            //act
            var result = _controller.Create();
            //assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Create_InvalidModelState_ReturnView()
        {
            //arrange
            _controller.ModelState.AddModelError("Error", "Error");
            //act
            var result = await _controller.Create(It.IsAny<Message>());
            //assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Create_RedirectToIndex_ReturnView()
        {
            //arrange
            var message = new Message()
            {
                Id = 1,
                Description = "test",
                Status = MessageStatus.NEW
            };
            //act
            var result = await _controller.Create(It.IsAny<Message>());
            //assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        private void CreateAdminUser()
        {
            var mockAdmin = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "name"),
                new Claim(ClaimTypes.NameIdentifier, "email@email"),
                new Claim(ClaimTypes.Role, "Admin")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = mockAdmin }
            };
        }

        private void CreatenUser()
        {
            var mockUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "email@email"),
                new Claim(ClaimTypes.NameIdentifier, "1234"),
                new Claim(ClaimTypes.Role, "User")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = mockUser }
            };
        }
    }
}
