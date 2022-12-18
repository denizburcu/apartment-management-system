using ApartmentManagement.Core.Models;
using ApartmentManagement.Repository;
using ApartmentManagement.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;

namespace AparmentManagement.Test.Repositories
{
    public class MessageRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public MessageRepositoryTests()
        {
            _options = RepositoryHelper.ApplicationDbContextOptionsEfCoreInMemory();
            RepositoryHelper.CreateDataBaseEfCoreInMemory(_options);
        }

        [Fact]
        public async void GetAllByUserIdAndIncludeUserAsync_ShouldIncludeUser_ReturnMessages()
        {
            await using (var context = new ApplicationDbContext(_options))
            {
                //arrange
                var repo = new MessageRepository(context);
                //act
                var messages =(List<Message>)await repo.GetAllByUserIdAndIncludeUserAsync("02174cf0–9412–4cfe-afbf-59f706d72cf6");
                //assert
                Assert.NotNull(messages);
                Assert.Equal(2, messages.Count);

            }
        }

        [Fact]
        public async void GetByIdIncludeUser_ShouldIncludeUser_ReturnMessage()
        {
            await using (var context = new ApplicationDbContext(_options))
            {
                //arrange
                var repo = new MessageRepository(context);
                //act
                var message = await repo.GetByIdIncludeUser(6);
                //assert
                Assert.NotNull(message);
                Assert.Equal(6, message.Id);
                Assert.NotNull(message.User);
            }
        }

        [Fact]
        public async void GetAllIncludeUser_ShouldIncludeUser_ReturnMessages()
        {
            await using (var context = new ApplicationDbContext(_options))
            {
                //arrange
                var repo = new MessageRepository(context);
                //act
                var messages = (List<Message>) await repo.GetAllIncludeUser();
                //assert
                Assert.NotNull(messages);
                Assert.NotNull(messages[0].User);
            }
        }
    }
}
