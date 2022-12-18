using ApartmentManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentManagement.Repository.Seeds
{
    public class ApartmentSeed : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasData(new Apartment
            {
                Id = 1,
                Floor = 2,
                ApartmentNumber = 1,
                Type = "2+1",
                BlockNumber = 4,
                Status = Status.FULL,
                UserId = "02174cf0–9412–4cfe-afbf-53422d33cf6"
            },
            new Apartment
            {
                Id = 2,
                Floor = 7,
                ApartmentNumber = 3,
                Type = "1+1",
                BlockNumber = 5,
                Status = Status.FULL,
                UserId = "02174cf0–9412–4cfe-afbf-5fhdf6d33cf6"

            },
            new Apartment
            {
                Id = 3,
                Floor = 7,
                ApartmentNumber = 3,
                Type = "4+1",
                BlockNumber = 5,
                Status = Status.FULL,
                UserId= "02174cf0–9412–4cfe-afbf-591231sd6d33cf6"

            },
            new Apartment
            {
                Id = 4,
                Floor = 3,
                ApartmentNumber = 5,
                Type = "3+1",
                BlockNumber = 5,
                Status = Status.FULL,
                UserId = "02174cf0–9123xccfe-afbf-59f706d33cf6"
            },
            new Apartment
            {
                Id = 5,
                Floor = 3,
                ApartmentNumber = 5,
                Type = "2+1",
                BlockNumber = 5,
                Status = Status.FULL,
                UserId = "02174cf0–9cvbcds2-afbf-59f706d33cf6"
            },
            new Apartment
            {
                Id = 6,
                Floor = 3,
                ApartmentNumber = 7,
                Type = "5+1",
                BlockNumber = 5,
                Status = Status.EMPTY,
                UserId = "02174cf0–xcvds2e-afbf-59f706d33cf6"

            },
            new Apartment
            {
                Id = 7,
                Floor = 4,
                ApartmentNumber = 7,
                Type = "2+1",
                BlockNumber = 5,
                Status = Status.EMPTY
            },
            new Apartment
            {
                Id = 8,
                Floor = 4,
                ApartmentNumber = 8,
                Type = "2+1",
                BlockNumber = 5,
                Status = Status.EMPTY
            },
            new Apartment
            {
                Id = 9,
                Floor = 4,
                ApartmentNumber = 10,
                Type = "2+1",
                BlockNumber = 5,
                Status = Status.EMPTY
            },
            new Apartment
            {
                Id = 10,
                Floor = 6,
                ApartmentNumber = 10,
                Type = "4+1",
                BlockNumber = 5,
                Status = Status.EMPTY
            },
            new Apartment
            {
                Id = 11,
                Floor = 6,
                ApartmentNumber = 12,
                Type = "3+1",
                BlockNumber = 5,
                Status = Status.EMPTY
            });
        }
    }
}
