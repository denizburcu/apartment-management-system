using ApartmentManagement.Core.Models;
using ApartmentManagement.Repository;
using ApartmentManagement.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AparmentManagement.Test.Repositories
{
    public class ApartmentRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public ApartmentRepositoryTests()
        {
            _options = RepositoryHelper.ApplicationDbContextOptionsEfCoreInMemory();
            RepositoryHelper.CreateDataBaseEfCoreInMemory(_options);
        }

        [Fact]
        public async void GetAllIncludeUserAsync_ShouldIncludeUser_ReturnApartments()
        {
            await using (var context = new ApplicationDbContext(_options))
            {
                //arrange
                var repo = new ApartmentRepository(context);
                //act
                var apartments = (List<Apartment>)await repo.GetAllIncludeUserAsync();
                //assert
                Assert.NotNull(apartments);
                Assert.Equal(3, apartments.Count);
                Assert.NotNull(apartments[1].User);
            }
        }
    }
}
