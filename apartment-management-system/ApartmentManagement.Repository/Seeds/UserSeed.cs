using ApartmentManagement.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentManagement.Repository.Seeds
{
    internal class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //ADMIN SEED
            //GET EMAIL AND PASSWORD TO LOGIN
            var user = new User
            {
                Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                Email = "admin@aps.com",
                NormalizedEmail = "ADMIN@APS.COM",
                EmailConfirmed = true,
                Name = "Deniz",
                LastName = "Aydin",
                IdentityNumber = "4556565623",
                PhoneNumber = "5453500023",
                PlateNumber = "34BOS45",
                UserName = "admin",
                NormalizedUserName = "ADMIN"
            };

            PasswordHasher<User> ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, "admin");
            builder.HasData(user);

            builder.HasData(new User
            {
                Id = "02174cf0–9412–4cfe-afbf-53422d33cf6",
                Email = "luffytaro@aps.com",
                NormalizedEmail = "LUFFYTARO@APS.COM",
                EmailConfirmed = true,
                Name = "LuffyTaro",
                LastName = "AYDINORO",
                IdentityNumber = "452256565623",
                PhoneNumber = "5453500023",
                PlateNumber = "34BOS45",
                UserName = "user2",
                PasswordHash = ph.HashPassword(user, "test"),
                NormalizedUserName = "USER2"
            },
            new User
            {
                Id = "02174cf0–9412–4cfe-afbf-5fhdf6d33cf6",
                Email = "yokotoro@aps.com",
                NormalizedEmail = "YOKOTORO@APS.COM",
                EmailConfirmed = true,
                Name = "Yoko",
                LastName = "Baygın",
                IdentityNumber = "452256565623",
                PhoneNumber = "5453500023",
                PlateNumber = "34BOS45",
                UserName = "user3",
                PasswordHash = ph.HashPassword(user, "test"),
                NormalizedUserName = "USER3"
            },
            new User
            {
                Id = "02174cf0–9412–4cfe-afbf-591231sd6d33cf6",
                Email = "ahmet@aps.com",
                NormalizedEmail = "AHMET@APS.COM",
                EmailConfirmed = true,
                Name = "ahmet",
                LastName = "deli",
                IdentityNumber = "452256565623",
                PhoneNumber = "5453500023",
                PlateNumber = "34BOS45",
                UserName = "user4",
                NormalizedUserName = "USER4",
                PasswordHash = ph.HashPassword(user, "test"),

            },
            new User
            {
                Id = "02174cf0–9123xccfe-afbf-59f706d33cf6",
                Email = "meltem@aps.com",
                NormalizedEmail = "MELTEM@APS.COM",
                EmailConfirmed = true,
                Name = "meltem",
                LastName = "cumbuş",
                IdentityNumber = "452256565623",
                PhoneNumber = "5453500023",
                PlateNumber = "34BOS45",
                UserName = "user5",
                NormalizedUserName = "USER5",
                PasswordHash = ph.HashPassword(user, "test"),

            },
            new User
            {
                Id = "02174cf0–9cvbcds2-afbf-59f706d33cf6",
                Email = "akin@aps.com",
                NormalizedEmail = "AKIN@APS.COM",
                EmailConfirmed = true,
                Name = "Akin",
                LastName = "Akmaz",
                IdentityNumber = "452256565623",
                PhoneNumber = "5453500023",
                PlateNumber = "34BOS45",
                UserName = "user6",
                NormalizedUserName = "USER6",
                PasswordHash = ph.HashPassword(user, "test"),

            },
            new User
            {
                Id = "02174cf0–xcvds2e-afbf-59f706d33cf6",
                Email = "mori@aps.com",
                NormalizedEmail = "MORI@APS.COM",
                EmailConfirmed = true,
                Name = "Mori",
                LastName = "Morar",
                IdentityNumber = "452256565623",
                PhoneNumber = "5453500023",
                PlateNumber = "34BOS45",
                UserName = "user7",
                NormalizedUserName = "USER7",
                PasswordHash = ph.HashPassword(user, "test"),
            });
        }
    }
}
