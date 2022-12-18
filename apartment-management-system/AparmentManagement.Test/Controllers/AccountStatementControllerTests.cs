using ApartmentManagement.Core.Contracts;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Web.Controllers;
using ApartmentManagement.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AparmentManagement.Test.Controllers
{
    public class AccountStatementControllerTests
    {
        private readonly Mock<IApartmentCostService> _mockService;

        private readonly AccountStatementController _controller;

        public AccountStatementControllerTests()
        {
            _mockService = new Mock<IApartmentCostService>();
            _controller = new AccountStatementController(_mockService.Object);
        }

        [Fact]
        public async void Index_ActionExecutes_ReturnView()
        {
            //arrange
            var aparmentCosts = CreateApartmentCostList();
            _mockService.Setup(m => m.GetAllApartmentCostsByPaid(true)).ReturnsAsync(aparmentCosts);
            //act
            var result = await _controller.Index();
            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ApartmentCostExportViewModel>(viewResult.Model);
            Assert.Equal(ExportFileType.EXCEL, model.SelectedFileType);
            Assert.Equal(5, model.ApartmentCosts.Count());
        }

        [Fact]
        public async void Index_GetsExcel_ReturnExcelResult()
        {
            //arrange
            var model = new ApartmentCostExportViewModel();
            //act
            var result = await _controller.Index(model);

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


    }
}
