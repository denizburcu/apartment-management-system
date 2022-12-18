using ApartmentManagement.Core.Services;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Core.ViewModels;
using ApartmentManagement.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AparmentManagement.Test.Controllers
{
    public class ApartmentControllerTests
    {
        private readonly Mock<IApartmentService> _mockApartmentService;
        private readonly Mock<IUserService> _mockUserService;

        private readonly ApartmentController _controller;

        public ApartmentControllerTests()
        {
            _mockApartmentService = new Mock<IApartmentService>();
            _mockUserService = new Mock<IUserService>();

            _controller = new ApartmentController(_mockApartmentService.Object, _mockUserService.Object);
        }

        [Fact]
        public async void Index_ActionExecutes_ReturnView()
        {
            //arrange
            var apartments = CreateFullApartmentList();
            _mockApartmentService.Setup(a => a.GetAll()).ReturnsAsync(apartments);
            //act
            var result = await _controller.Index();
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var actualAparments = Assert.IsAssignableFrom<IEnumerable<Apartment>>(viewResult.Model);
            Assert.Equal(2, actualAparments.Count());
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
            var result = await _controller.Create(CreateFullApartment());
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Create_RedirectAction_ReturnIndex()
        {
            //arrange
            var apartment = CreateFullApartment();
            _mockApartmentService.Setup(m => m.AddApartment(apartment)).Returns(Task.CompletedTask);
            //act
            var result = await _controller.Create(apartment);
            //assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", viewResult.ActionName);
            _mockApartmentService.Verify(m => m.AddApartment(apartment), Times.Once);

        }

        [Fact]
        public async void Edit_GetsApartmentAction_ReturnView()
        {
            //arrange
            var apartment = CreateEmptyApartment();
            var userList = CreateUserList();
            _mockApartmentService.Setup(a => a.GetById(1)).ReturnsAsync(apartment);
            _mockUserService.Setup(u => u.GetAllNonResidentUsers()).ReturnsAsync(userList);
            //act
            var result = await _controller.Edit(1);
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<UserApartmentViewModel>(viewResult.Model);
            Assert.NotNull(model);
            Assert.Equal(apartment.Id, model.Apartment.Id);
            Assert.Equal(2, model.AparmentUsers.Count());
        }

        [Fact]
        public async void Edit_ActionExecutes_RreturnView()
        {
            //arrange
            _controller.ModelState.AddModelError("Error", "Error");
            //act
            var result = await _controller.Edit(CreateUserApartmentViewModel());
            //assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Edit_ActionExecutes_ReturnIndex()
        {
            //arrange
            var model = CreateUserApartmentViewModel();
            _mockApartmentService.Setup(m => m.UpdateApartment(It.IsAny<Apartment>())).Returns(Task.CompletedTask);
            //act
            var result = await _controller.Edit(model);
            //assert
            var redirectedResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectedResult.ActionName);
            _mockApartmentService.Verify(m => m.UpdateApartment(model.Apartment), Times.Once);
        }

        [Fact]
        public async void Delete_ActionExecutes_RreturnView()
        {
            //arrange
            var apartment = CreateEmptyApartment();
            _mockApartmentService.Setup(m => m.GetById(1)).ReturnsAsync(apartment);
            //act
            var result = await _controller.Delete(1);
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var actualAparment = Assert.IsAssignableFrom<Apartment>(viewResult.Model);
            Assert.NotNull(actualAparment);
        }

        [Fact]
        public async void DeletePost_ActionExecutes_ReturnIndex()
        {
            //arrange
            var id = 0;
            _mockApartmentService.Setup(m => m.RemoveApartment(1)).Callback<int>(a => a = id);
            //act
            var result = await _controller.DeletePost(id);
            //assert
            var redirectedResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectedResult.ActionName);
            _mockApartmentService.Verify(m => m.RemoveApartment(id), Times.Once);
        }

        private UserApartmentViewModel CreateUserApartmentViewModel()
        {
            return new UserApartmentViewModel()
            {
                Apartment = CreateEmptyApartment(),
                AparmentUsers = null
            };
        }

        private List<User> CreateUserList()
        {
            return new List<User>()
            {
            new User
            {
                Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                Email = "test@aps.com",
                NormalizedEmail = "TEST@APS.COM",
                EmailConfirmed = true,
                Name = "Test",
                LastName = "Aydin",
                IdentityNumber = "4556565623",
                PhoneNumber = "5453500023",
                PlateNumber = "34BOS45",
                UserName = "user",
                NormalizedUserName = "USER"
            },
            new User
            {
                Id = "02174cf0–9412–4czxse-afbf-59f706d72cf6",
                Email = "sets@aps.com",
                NormalizedEmail = "SETS@APS.COM",
                EmailConfirmed = true,
                Name = "Sest",
                LastName = "Aydin",
                IdentityNumber = "4556565623",
                PhoneNumber = "5453500023",
                PlateNumber = "34BOS45",
                UserName = "user",
                NormalizedUserName = "USER"
            }
            };
        }

        private Apartment CreateEmptyApartment()
        {
            return new Apartment()
            {
                Id = 1,
                BlockNumber = 2,
                ApartmentNumber = 3,
                Type = "2+1",
                Status = Status.EMPTY
            };
        }

        private Apartment CreateFullApartment()
        {
            return new Apartment()
            {
                Id = 1,
                BlockNumber = 2,
                ApartmentNumber = 3,
                Type = "2+1",
                UserId = "1234",
                Status = Status.FULL
            };
        }

        private List<Apartment> CreateFullApartmentList()
        {
            return new List<Apartment>()
            {
                new Apartment()
                {
                    Id = 1,
                    BlockNumber = 2,
                    ApartmentNumber = 3,
                    Type = "2+1",
                    UserId = "123",
                    Status = Status.FULL
                },
                new Apartment()
                {
                    Id = 2,
                    BlockNumber = 3,
                    ApartmentNumber = 5,
                    Type = "3+1",
                    UserId = "1234",
                    Status = Status.FULL
                },
            };
        }

    }
}
