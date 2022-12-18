using AparmentManagement.Test.Helpers;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Web.Controllers;
using ApartmentManagement.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AparmentManagement.Test.Controllers
{
    public class LoginControllerTests
    {
        private readonly Mock<FakeUserManager> _mockUserManager;
        private readonly Mock<FakeSignInManager> _mockSignInManager;

        private readonly LoginController _controller;

        public LoginControllerTests()
        {
            _mockUserManager = new Mock<FakeUserManager>();
            _mockSignInManager = new Mock<FakeSignInManager>();

            _controller = new LoginController(_mockUserManager.Object, _mockSignInManager.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnView()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Index_InvalidModel_ReturnView()
        {
            //arrange
            var model = CreateUserLoginViewModel();
            _controller.ModelState.AddModelError("Error", "Error");
            //act
            var result = await _controller.Index(model);
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewResult.ViewData.ModelState["Login"].Errors[0].ErrorMessage, "Giriş yapılamadı");

        }

        [Fact]
        public async void Index_Invaliduser_ReturnView()
        {
            //arrange
            var model = CreateUserLoginViewModel();
            _mockUserManager.Setup(m => m.FindByEmailAsync(model.Email));
            //act
            var result = await _controller.Index(model);
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewResult.ViewData.ModelState["Login"].Errors[0].ErrorMessage, "Kullanıcı bulunamadı");
        }

        [Fact]
        public async void Index_InvalidPasword_ReturnView()
        {
            //arrange
            var model = CreateUserLoginViewModel();
            var user = new User();
            _mockUserManager.Setup(m => m.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(m => m.CheckPasswordAsync(user, model.Password)).ReturnsAsync(false);
            //act
            var result = await _controller.Index(model);
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewResult.ViewData.ModelState["Login"].Errors[0].ErrorMessage, "Şifre yanlış");
        }

        [Fact]
        public async void Logout_ActionExecutes_ReturnRedirect()
        {
            //arrange
            _mockSignInManager.Setup(m => m.SignOutAsync());
            //act
            var result = await _controller.Logout();
            //assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", viewResult.ActionName);
            Assert.Equal("Home", viewResult.ControllerName);
        }

        private UserLoginViewModel CreateUserLoginViewModel()
        {
            return new UserLoginViewModel()
            {
                Email = "email@email.com",
                Password = "password"
            };
        }
    }
}
