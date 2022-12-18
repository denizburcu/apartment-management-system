using ApartmentManagement.Core.Models;
using ApartmentManagement.Repository;
using ApartmentManagement.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;

namespace AparmentManagement.Test.Repositories
{
    public class ApartmentCostRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public ApartmentCostRepositoryTests()
        {
            _options = RepositoryHelper.ApplicationDbContextOptionsEfCoreInMemory();
            RepositoryHelper.CreateDataBaseEfCoreInMemory(_options);
        }

        [Fact]
        public async void GetAllByIsPaidIncludeApartmentAsync_ShouldIncludeApartment_ReturnApartments()
        {
            await using (var context = new ApplicationDbContext(_options))
            {
                //arrange
                var repo = new ApartmentCostRepository(context);
                //act
                var costs = (List<ApartmentCost>)await repo.GetAllByIsPaidIncludeApartmentAsync(true);
                //assert
                Assert.NotNull(costs);
                Assert.Equal(2, costs.Count);
                Assert.NotNull(costs[0].Apartment);

            }
        }

        [Fact]
        public async void GetAllByUserId_ShouldIncludeApartment_ReturnApartmentCosts()
        {
            await using (var context = new ApplicationDbContext(_options))
            {
                //arrange
                var repo = new ApartmentCostRepository(context);
                //act
                var costs = (List<ApartmentCost>)await repo.GetAllByUserId("02174cf0–9412–4cfe-afbf-59f706d33cf6");
                //assert
                Assert.NotNull(costs);
                Assert.NotNull(costs[0].Apartment);

            }
        }

        [Fact]
        public async void GetAllEmailsByNotPaidApartmentCostsAsync_ShouldGetEmailFromCosts_ReturnEmails()
        {
            await using (var context = new ApplicationDbContext(_options))
            {
                //arrange
                var repo = new ApartmentCostRepository(context);
                //act
                var emails = (List<string>)await repo.GetAllEmailsByNotPaidApartmentCostsAsync();
                //assert
                Assert.NotNull(emails);
                Assert.Equal(2, emails.Count);
            }
        }

        [Fact]
        public async void GetAllNotPaidCostsByMonthIncludeApartmentAsync_ShouldGetCostByMonthlyIncludeApartment_ReturnApartmentCosts()
        {
            await using (var context = new ApplicationDbContext(_options))
            {
                //arrange
                var repo = new ApartmentCostRepository(context);
                //act
                var costs = (List<ApartmentCost>)await repo.GetAllNotPaidCostsByMonthIncludeApartmentAsync(Month.DECEMBER);
                //assert
                Assert.NotNull(costs);
                Assert.Equal(4, costs.Count);
                Assert.NotNull(costs[0].Apartment);
            }
        }

        [Fact]
        public async void GetAllPaidOrderByDescendingAsync_GetCostsInOrder_ReturnApartmentCosts()
        {
            await using (var context = new ApplicationDbContext(_options))
            {
                //arrange
                var repo = new ApartmentCostRepository(context);
                //act
                var costs = (List<ApartmentCost>)await repo.GetAllPaidOrderByDescendingAsync();
                //assert
                Assert.NotNull(costs);
                Assert.Equal(2, costs.Count);
                Assert.Equal(6, costs[0].Id);
                Assert.Equal(7, costs[1].Id);
            }
        }

        [Fact]
        public async void GetByIdIncludeAparmentAsync_GetsCostsByIdIncludeApartment_ReturnApartmentCost()
        {
            await using (var context = new ApplicationDbContext(_options))
            {
                //arrange
                var repo = new ApartmentCostRepository(context);
                //act
                var cost = await repo.GetByIdIncludeAparmentAsync(4);
                //assert
                Assert.NotNull(cost);
                Assert.Equal(4, cost.Id);
                Assert.NotNull(cost.Apartment);
            }
        }
    }
}
