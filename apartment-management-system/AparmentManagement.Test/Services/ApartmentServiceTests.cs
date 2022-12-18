using ApartmentManagement.Core.IUnitOfWorks;
using ApartmentManagement.Core.Models;
using ApartmentManagement.Core.Repositories;
using ApartmentManagement.Service.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AparmentManagement.Test.Services
{
    public class ApartmentServiceTests
    {
        private readonly Mock<IApartmentRepository> _mockRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly ApartmentService _service;
        private readonly List<Apartment> _list;

        public ApartmentServiceTests()
        {
            _mockRepo = new Mock<IApartmentRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new ApartmentService(_mockRepo.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task AddApartment_ShouldAdd_ReturnVoid()
        {
            //arrange
            var apartment = CreateEmptyApartment();
            _mockRepo.Setup(g => g.AddAsync(apartment)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);
            //act
            await _service.AddApartment(apartment);
            //assert
            _mockRepo.Verify(g => g.AddAsync(apartment));
            _mockUnitOfWork.Verify(g => g.CommitAsync());
        }

        [Fact]
        public async Task RmoveApartment_ShouldRemoveAparment_ReturnVoid()
        {
            //arrange
            var apartment = CreateEmptyApartment();
            _mockRepo.Setup(g => g.Remove(apartment));
            _mockRepo.Setup(g => g.GetByIdAsync(1)).ReturnsAsync(apartment);
            _mockUnitOfWork.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);
            //act
            await _service.RemoveApartment(1);
            //assert
            _mockRepo.Verify(g => g.Remove(apartment));
            _mockUnitOfWork.Verify(g => g.CommitAsync());
        }

        [Fact]
        public async Task GetAll_AllApartments_ReturnList()
        {
            //arrange
            var apartments = CreateFullApartmentList();
            _mockRepo.Setup(r => r.GetAllIncludeUserAsync()).ReturnsAsync(apartments);
            //act
            var result = await _service.GetAll();
            //assert
            _mockRepo.Verify(r => r.GetAllIncludeUserAsync());
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateApartment_ShouldUpdateApartment_ReturnVoid()
        {
            //arrange
            var apartment = CreateFullApartment();
            _mockRepo.Setup(m => m.Update(It.IsAny<Apartment>()));
            _mockUnitOfWork.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);
            //act
            await _service.UpdateApartment(apartment);
            //assert
            _mockRepo.Verify(m => m.Update(It.IsAny<Apartment>()), Times.Once);
            Assert.Equal(Status.FULL, apartment.Status);
        }

        [Fact]
        public async Task GetById_ShouldGetApartment_ReturnApartment()
        {
            //arrange
            var apartment = CreateFullApartment();
            _mockRepo.Setup(m => m.GetByIdAsync(1)).ReturnsAsync(apartment);
            //act
            var result = await _service.GetById(1);
            //assert
            _mockRepo.Verify(m => m.GetByIdAsync(1), Times.Once);
            Assert.NotNull(result);
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
                Status = Status.EMPTY
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
