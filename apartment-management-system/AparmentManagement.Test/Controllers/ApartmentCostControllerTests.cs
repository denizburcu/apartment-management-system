using ApartmentManagement.Core.Contracts;
using ApartmentManagement.Core.DTOs;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Web.Controllers;
using ApartmentManagement.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace AparmentManagement.Test.Controllers
{
    public class ApartmentCostControllerTests
    {
        private readonly Mock<IApartmentService> _mockApartmentService;
        private readonly Mock<IApartmentCostService> _mockApartmentCostService;

        private readonly ApartmentCostController _controller;

        public ApartmentCostControllerTests()
        {
            _mockApartmentService = new Mock<IApartmentService>();
            _mockApartmentCostService = new Mock<IApartmentCostService>();

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            _controller = new ApartmentCostController(_mockApartmentCostService.Object, _mockApartmentService.Object)
            {
                TempData = tempData
            };
        }

        [Fact]
        public async void Index_ActionExecutesAdmin_ReturnView()
        {
            //arrange
            CreateAdminUser();
            var apartmentCost = CreateApartmentCostList();
            _mockApartmentCostService.Setup(m => m.GetAllApartmentCostByMonth(Month.OCTOBER)).ReturnsAsync(apartmentCost);
            //act
            var result = await _controller.Index();
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ApartmentCostMonthViewModel>(viewResult.Model);
            Assert.Equal(Month.OCTOBER, model.SelectedMonth);
            Assert.Equal(5, model.ApartmentCosts.Count());
        }

        [Fact]
        public void Index_RedirectToIndex_ReturnRedirectResult()
        {
            //arrange
            var model = new ApartmentCostMonthViewModel()
            {
                SelectedMonth = Month.DECEMBER
            };
            //act
            var result = _controller.Index(model);
            //assert
            var redirectedResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectedResult.ActionName);
            Assert.Equal(_controller.TempData["SelectedMonth"], Month.DECEMBER);
        }

        [Fact]
        public async void Create_ActionExecutes_ReturnViews()
        {
            //arrange
            _mockApartmentService.Setup(a => a.GetAll());
            //act
            var result = await _controller.Create();
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ApartmentCostViewModel>(viewResult.Model);
            Assert.NotNull(model);
        }

        [Fact]
        public async void Create_CreateNewCost_ReturnIndex()
        {
            //arrange
            var apartmentCost = CreateApartmentCost();
            var model = new ApartmentCostViewModel()
            {
                ApartmentCost = apartmentCost,
            };
            _mockApartmentCostService.Setup(a => a.AddApartmentCost(apartmentCost));
            //act
            var result = await _controller.Create(model);
            //assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            _mockApartmentCostService.Verify(m => m.AddApartmentCost(apartmentCost), Times.Once);
        }

        [Fact]
        public async void Create_InvalidModel_ReturnIndex()
        {
            //arrange
            _controller.ModelState.AddModelError("Error", "Error");
            var apartmentCost = CreateApartmentCost();
            var model = new ApartmentCostViewModel()
            {
                ApartmentCost = apartmentCost,
            };
            //act
            var result = await _controller.Create(model);
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            _mockApartmentCostService.Verify(m => m.AddApartmentCost(apartmentCost), Times.Never);
        }

        [Fact]
        public async void Pay_ActionExecutes_ReturnView()
        {
            //arrange
            _mockApartmentService.Setup(a => a.GetById(1));
            //act
            var result = await _controller.Pay(1);
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ApartmentCostPayViewModel>(viewResult.Model);
            Assert.NotNull(model);
        }

        [Fact]
        public async void Pay_InValidPayment_ReturnView()
        {
            //arrange
            var model = new ApartmentCostPayViewModel()
            {
                ApartmentCost = CreateApartmentCost(),
                PaymentDto = new PaymentDto()
            };
            _mockApartmentCostService.Setup(m => m.PayApartmentCost(model.PaymentDto, 1)).ReturnsAsync(false);
            //act
            var result = await _controller.Pay(model);
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(viewResult.ViewData.ModelState["Payment"].Errors[0].ErrorMessage, "Ödeme yapılamadı");
        }

        [Fact]
        public async void Pay_ValidPayment_ReturnView()
        {
            //arrange
            var model = new ApartmentCostPayViewModel()
            {
                ApartmentCost = CreateApartmentCost(),
                PaymentDto = new PaymentDto()
            };
            _mockApartmentCostService.Setup(m => m.PayApartmentCost(model.PaymentDto, 1)).ReturnsAsync(true);
            //act
            var result = await _controller.Pay(model);
            //assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }


        private List<ApartmentCost> CreateApartmentCostList()
        {
            return new List<ApartmentCost>()
            {
                new ApartmentCost()
                {
                    Id = 1,
                    Amount = 123,
                    ApartmentId = 2,
                    CostType = CostType.ELECTRICITY,
                    IsPaid = false,
                    Month = Month.JANUARY
                },
                new ApartmentCost()
                {
                    Id = 2,
                    Amount = 123,
                    ApartmentId = 2,
                    CostType = CostType.ELECTRICITY,
                    IsPaid = false,
                    Month = Month.JANUARY
                },
                new ApartmentCost()
                {
                    Id = 3,
                    Amount = 123,
                    ApartmentId = 2,
                    CostType = CostType.ELECTRICITY,
                    IsPaid = false,
                    Month = Month.JANUARY
                },
                new ApartmentCost()
                {
                    Id = 4,
                    Amount = 123,
                    ApartmentId = 2,
                    CostType = CostType.WATER,
                    IsPaid = false,
                    Month = Month.OCTOBER
                },
                new ApartmentCost()
                {
                    Id = 5,
                    Amount = 123,
                    ApartmentId = 2,
                    CostType = CostType.DUES,
                    IsPaid = false,
                    Month = Month.OCTOBER
                }
            };
        }

        private void CreateAdminUser()
        {
            var mockAdmin = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "name"),
                new Claim(ClaimTypes.NameIdentifier, "email@email"),
                new Claim(ClaimTypes.Role, "Admin")
            }, "mock"));
            var httpContext = new DefaultHttpContext() { User = mockAdmin };
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
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

        private ApartmentCost CreateApartmentCost()
        {
            return new ApartmentCost()
            {
                Id = 1,
                Amount = 123,
                ApartmentId = 2,
                CostType = CostType.ELECTRICITY,
                IsPaid = false,
                Month = Month.JANUARY
            };
        }
    }
}
