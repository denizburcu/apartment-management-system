using ApartmentManagement.Core.Models;
using ApartmentManagement.Repository;
using Microsoft.EntityFrameworkCore;
using System;

namespace AparmentManagement.Test.Repositories
{
    public static class RepositoryHelper
    {
        public static DbContextOptions<ApplicationDbContext> ApplicationDbContextOptionsEfCoreInMemory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase($"AMSDatabase{Guid.NewGuid()}")
                .Options;

            return options;
        }

        public static async void CreateDataBaseEfCoreInMemory(DbContextOptions<ApplicationDbContext> options)
        {
            await using (var context = new ApplicationDbContext(options))
            {
                CreateData(context);
            }
        }

        private static void CreateData(ApplicationDbContext apartmentDAB)
        {
            apartmentDAB.Apartment.Add(new Apartment { Id = 1, BlockNumber = 1, Floor = 2, ApartmentNumber = 5, Status = Status.EMPTY, Type = "2+1" });
            apartmentDAB.Apartment.Add(new Apartment { Id = 2, UserId = "02174cf0–9412–4cfe-afbf-59f706d33cf6", BlockNumber = 31, Floor = 3, ApartmentNumber = 7, Status = Status.EMPTY, Type = "2+1" });
            apartmentDAB.Apartment.Add(new Apartment { Id = 3, UserId = "02174cf0–9412–4cfe-afbf-59f706d72cf6", BlockNumber = 15, Floor = 4, ApartmentNumber = 8, Status = Status.EMPTY, Type = "2+1" });
            apartmentDAB.Users.Add(new User
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
            });
            apartmentDAB.Users.Add(new User
            {
                Id = "02174cf0–9412–4cfe-afbf-59f706d33cf6",
                Email = "test2@aps.com",
                NormalizedEmail = "TEST2@APS.COM",
                EmailConfirmed = true,
                Name = "Test",
                LastName = "SAYDGN",
                IdentityNumber = "452256565623",
                PhoneNumber = "5453500023",
                PlateNumber = "34BOS45",
                UserName = "user",
                NormalizedUserName = "USER"
            });
            apartmentDAB.Message.Add(new Message { Id = 2, Description = "Faturaları ödedim", UserId = "02174cf0–9412–4cfe-afbf-59f706d33cf6", Status = MessageStatus.NEW });
            apartmentDAB.Message.Add(new Message { Id = 6, Description = "Faturaları ödedim222", UserId = "02174cf0–9412–4cfe-afbf-59f706d33cf6", Status = MessageStatus.NEW });
            apartmentDAB.Message.Add(new Message { Id = 3, Description = "Copleri cikardım", UserId = "02174cf0–9412–4cfe-afbf-59f706d33cf6", Status = MessageStatus.NEW });
            apartmentDAB.Message.Add(new Message { Id = 4, Description = "Faturaları ödedim", UserId = "02174cf0–9412–4cfe-afbf-59f706d72cf6", Status = MessageStatus.READ });
            apartmentDAB.Message.Add(new Message { Id = 5, Description = "Faturaları ödedim", UserId = "02174cf0–9412–4cfe-afbf-59f706d72cf6", Status = MessageStatus.READ });
                        apartmentDAB.ApartmentCost.Add(new ApartmentCost {
                Id = 3,
                ApartmentId = 3,
                Month = Month.DECEMBER,
                IsPaid = false,
                Amount = 250,
                CostType = CostType.WATER
            });
            apartmentDAB.ApartmentCost.Add(new ApartmentCost {
                Id = 2,
                ApartmentId = 3,
                Month = Month.DECEMBER,
                IsPaid = false,
                Amount = 250,
                CostType = CostType.WATER
            });

            apartmentDAB.ApartmentCost.Add(new ApartmentCost {
                Id = 4,
                ApartmentId = 2,
                Month = Month.DECEMBER,
                IsPaid = false,
                Amount = 250,
                CostType = CostType.WATER
            });
            apartmentDAB.ApartmentCost.Add(new ApartmentCost {
                Id = 5,
                ApartmentId = 2,
                Month = Month.DECEMBER,
                IsPaid = false,
                Amount = 250,
                CostType = CostType.WATER
            });
            apartmentDAB.ApartmentCost.Add(new ApartmentCost
            {
                Id = 7,
                ApartmentId = 2,
                Month = Month.DECEMBER,
                IsPaid = true,
                Amount = 250,
                CostType = CostType.WATER
            });
            apartmentDAB.ApartmentCost.Add(new ApartmentCost
            {
                Id = 6,
                ApartmentId = 2,
                Month = Month.DECEMBER,
                IsPaid = true,
                Amount = 250,
                CostType = CostType.WATER
            });


            apartmentDAB.SaveChangesAsync();
        }
    }
}
