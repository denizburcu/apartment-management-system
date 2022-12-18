using ApartmentManagement.Core.DTOs;
using ApartmentManagement.Core.IUnitOfWorks;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Core.Repositories;
using ApartmentManagement.Service.Services;
using Fingers10.ExcelExport.ActionResults;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AparmentManagement.Test.Services
{
    public class ApartmentCostsServiceTests
    {
        private readonly Mock<IApartmentCostRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<CreditCardClientService> _mockCreditCardClientService;

        private readonly ApartmentCostService _service;
        private readonly List<Apartment> _list;

        public ApartmentCostsServiceTests()
        {
            _mockRepo = new Mock<IApartmentCostRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCreditCardClientService = new Mock<CreditCardClientService>();
            _service = new ApartmentCostService(_mockRepo.Object, _mockUnitOfWork.Object, _mockCreditCardClientService.Object);
        }

        [Fact]
        public async Task AddApartmentCost_ShouldAdd_ReturnVoid()
        {
            //arrange
            var apartmentCost = CreateApartmentCost();
            _mockRepo.Setup(g => g.AddAsync(apartmentCost)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);
            //act
            await _service.AddApartmentCost(apartmentCost);
            //assert
            _mockRepo.Verify(g => g.AddAsync(apartmentCost));
            _mockUnitOfWork.Verify(g => g.CommitAsync());
        }

        [Fact]
        public async Task ExportApartmentCostByFileType_ShouldGetAllPaidCosts_ReturnExcel()
        {
            //arrange
            var apartmentCosts = CreatePaidApartmentCostList();
            _mockRepo.Setup(g => g.GetAllPaidOrderByDescendingAsync()).ReturnsAsync(apartmentCosts);
            //act
            var result = (ExcelResult<ApartmentCost>) await _service.ExportApartmentCostByFileType(ExportFileType.EXCEL);
            //assert
            Assert.Equal("PaidCostReport", result.FileName);
            Assert.Equal("sheet1", result.SheetName);
        }

        [Fact]
        public async Task GetAllApartmentCostByMonth_ShouldGetAllCostByMonthly_ReturnCosts()
        {
            //arrange
            var apartmentCosts = CreateApartmentCostList();
            _mockRepo.Setup(r => r.GetAllNotPaidCostsByMonthIncludeApartmentAsync(Month.OCTOBER)).ReturnsAsync(apartmentCosts);
            //act
            var result = await _service.GetAllApartmentCostByMonth(Month.OCTOBER);
            //assert
            _mockRepo.Verify(r => r.GetAllNotPaidCostsByMonthIncludeApartmentAsync(Month.OCTOBER));
        }

        [Fact]
        public async Task GetAllApartmentCostsByPaid_ShouldGetAllCostByPaid_ReturnCosts()
        {
            //arrange
            var apartmentCosts = CreateApartmentCostList();
            _mockRepo.Setup(r => r.GetAllByIsPaidIncludeApartmentAsync(true)).ReturnsAsync(apartmentCosts);
            //act
            var result = await _service.GetAllApartmentCostsByPaid(true);
            //assert
            _mockRepo.Verify(r => r.GetAllByIsPaidIncludeApartmentAsync(true));
        }

        [Fact]
        public async Task GetAllApartmentCostsByUser_ShouldGetAllCostByUserId_ReturnCosts()
        {
            //arrange
            var apartmentCosts = CreateApartmentCostList();
            _mockRepo.Setup(r => r.GetAllByUserId("123")).ReturnsAsync(apartmentCosts);
            //act
            var result = await _service.GetAllApartmentCostsByUser("123");
            //assert
            _mockRepo.Verify(r => r.GetAllByUserId("123"));
        }

        [Fact]
        public async Task GetAllEmailByUnpaidApartmentCosts_ShouldGetAllCostByMonthly_ReturnsEmails()
        {
            //arrange
            var apartmentCosts = new List<string>()
            {
                "deniz@gmail.com",
                "luffy@gmail.com"
            };
            _mockRepo.Setup(r => r.GetAllEmailsByNotPaidApartmentCostsAsync()).ReturnsAsync(apartmentCosts);
            //act
            var result = await _service.GetAllEmailByUnpaidApartmentCosts();
            //assert
            _mockRepo.Verify(r => r.GetAllEmailsByNotPaidApartmentCostsAsync());
        }

        [Fact]
        public async Task PayApartmentCost_ShouldUpdateCostsStatus_ReturnsBool()
        {
            /*//arrange
            var paymentDto = new PaymentDto()
            {
                CardNumber = "1234567890123456",
                CvcCvv= "123",
                ExpireDate = "12/20",
                PaidAmount = 100
            };
            var apartmentCost = CreateApartmentCost();
            _mockCreditCardClientService.Setup(c => c.MakePayment(paymentDto)).ReturnsAsync(true);
            _mockRepo.Setup(r => r.GetByIdIncludeAparmentAsync(1)).ReturnsAsync(apartmentCost);
            _mockRepo.Setup(m => m.Update(It.IsAny<ApartmentCost>()));
            _mockUnitOfWork.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);
            //act
            var result = await _service.PayApartmentCost(paymentDto, 1);
            //assert
            _mockCreditCardClientService.Verify(r => r.MakePayment(paymentDto), Times.Once);
            _mockRepo.Verify(r => r.Update(It.IsAny<ApartmentCost>()), Times.Once);
            Assert.True(result);*/

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

        private List<ApartmentCost> CreatePaidApartmentCostList()
        {
            return new List<ApartmentCost>()
            {
                new ApartmentCost()
                {
                    Id = 1,
                    Amount = 123,
                    ApartmentId = 2,
                    CostType = CostType.ELECTRICITY,
                    IsPaid = true,
                    Month = Month.JANUARY
                },
                new ApartmentCost()
                {
                    Id = 2,
                    Amount = 123,
                    ApartmentId = 2,
                    CostType = CostType.ELECTRICITY,
                    IsPaid = true,
                    Month = Month.JANUARY
                },
                new ApartmentCost()
                {
                    Id = 3,
                    Amount = 123,
                    ApartmentId = 2,
                    CostType = CostType.ELECTRICITY,
                    IsPaid = true,
                    Month = Month.JANUARY
                },
                new ApartmentCost()
                {
                    Id = 4,
                    Amount = 123,
                    ApartmentId = 2,
                    CostType = CostType.WATER,
                    IsPaid = true,
                    Month = Month.OCTOBER
                },
                new ApartmentCost()
                {
                    Id = 5,
                    Amount = 123,
                    ApartmentId = 2,
                    CostType = CostType.DUES,
                    IsPaid = true,
                    Month = Month.OCTOBER
                }
            };
        }

    }
}
