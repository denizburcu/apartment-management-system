using ApartmentManagement.Core.Models;
using ApartmentManagement.Repository;
using ApartmentManagement.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;

namespace AparmentManagement.Test.Repositories
{
    public class GenericRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public GenericRepositoryTests()
        {
            _options = RepositoryHelper.ApplicationDbContextOptionsEfCoreInMemory();
            RepositoryHelper.CreateDataBaseEfCoreInMemory(_options);
        }

        [Fact]
        public async void AddAsync_AddEntity_ReturnVoid()
        {
            Message messageAdded = new Message() { Id = 1, Description = "Faturaları ödedim", UserId = "02174cf0–9412–4cfe-afbf-59f706d33cf6", Status = MessageStatus.NEW };


            await using (var context = new ApplicationDbContext(_options))
            {
                var repository = new GenericRepository<Message>(context);
                await repository.AddAsync(messageAdded);
                context.SaveChanges();

                var result = await repository.GetByIdAsync(1);
                Assert.NotNull(result);
                Assert.Equal(1, result.Id);
                Assert.Equal(messageAdded.Description, result.Description);
            }
        }

        [Fact]
        public async void Remove_Entity_ReturnVoid()
        {
            Message messageRemoved = new Message() { Id = 2, Description = "Faturaları ödedim", UserId = "02174cf0–9412–4cfe-afbf-59f706d33cf6", Status = MessageStatus.NEW };

            await using (var context = new ApplicationDbContext(_options))
            {
                var repository = new GenericRepository<Message>(context);
                repository.Remove(messageRemoved);
                context.SaveChanges();

                var result = await repository.GetByIdAsync(2);
                Assert.Null(result);
            }
        }

        [Fact]
        public async void Update_Entity_ReturnVoid()
        {
            Message messageUpdated = new Message() { Id = 2, Description = "Faturaları ödedimmmm", UserId = "02174cf0–9412–4cfe-afbf-59f706d33cf6", Status = MessageStatus.NEW };

            await using (var context = new ApplicationDbContext(_options))
            {
                var repository = new GenericRepository<Message>(context);
                repository.Update(messageUpdated);
                context.SaveChanges();

                var result = await repository.GetByIdAsync(2);
                Assert.NotNull(result);
                Assert.Equal(messageUpdated.Description, result.Description);
            }
        }

        [Fact]
        public async void GetAll_ShouldGetAllEntities_ReturnLists()
        {
            await using (var context = new ApplicationDbContext(_options))
            {
                var repository = new GenericRepository<Message>(context);
                var result = (List<Message>) await repository.GetAll();

                Assert.NotNull(result);
                Assert.Equal(5, result.Count);
            }
        }
    }
}
