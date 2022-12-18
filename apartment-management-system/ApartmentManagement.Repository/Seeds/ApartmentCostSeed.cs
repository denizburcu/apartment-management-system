using ApartmentManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentManagement.Repository.Seeds
{
    internal class ApartmentCostSeed : IEntityTypeConfiguration<ApartmentCost>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ApartmentCost> builder)
        {
            builder.HasData(new ApartmentCost
                            {
                                Id = 1,
                                ApartmentId = 1,
                                Month = Month.DECEMBER,
                                IsPaid = false,
                                Amount = 250,
                                CostType = CostType.ELECTRICITY

                            },
                            new ApartmentCost
                            {
                                Id = 2,
                                ApartmentId = 1,
                                Month = Month.DECEMBER,
                                IsPaid = false,
                                Amount = 250,
                                CostType = CostType.WATER

                            },
                            new ApartmentCost
                            {
                                Id = 3,
                                ApartmentId = 1,
                                Month = Month.DECEMBER,
                                IsPaid = false,
                                Amount = 250,
                                CostType = CostType.GAS

                            },
                            new ApartmentCost
                            {
                                Id = 4,
                                ApartmentId = 2,
                                Month = Month.SEPTEMBER,
                                IsPaid = false,
                                Amount = 250,
                                CostType = CostType.ELECTRICITY

                            },
                            new ApartmentCost
                            {
                                Id = 5,
                                ApartmentId = 2,
                                Month = Month.OCTOBER,
                                IsPaid = false,
                                Amount = 250,
                                CostType = CostType.GAS
                            },
                            new ApartmentCost
                            {
                                Id = 6,
                                ApartmentId = 2,
                                Month = Month.SEPTEMBER,
                                IsPaid = true,
                                Amount = 250,
                                CostType = CostType.GAS
                            },
                            new ApartmentCost
                            {
                                Id = 7,
                                ApartmentId = 3,
                                Month = Month.SEPTEMBER,
                                IsPaid = true,
                                Amount = 250,
                                CostType = CostType.GAS
                            },
                            new ApartmentCost
                            {
                                Id = 8,
                                ApartmentId = 3,
                                Month = Month.SEPTEMBER,
                                IsPaid = true,
                                Amount = 250,
                                CostType = CostType.GAS
                            },
                            new ApartmentCost
                            {
                                Id = 9,
                                ApartmentId = 4,
                                Month = Month.OCTOBER,
                                IsPaid = true,
                                Amount = 333,
                                CostType = CostType.ELECTRICITY
                            },
                            new ApartmentCost
                            {
                                Id = 10,
                                ApartmentId = 4,
                                Month = Month.OCTOBER,
                                IsPaid = true,
                                Amount = 22,
                                CostType = CostType.WATER
                            },
                            new ApartmentCost
                            {
                                Id = 11,
                                ApartmentId = 1,
                                Month = Month.DECEMBER,
                                IsPaid = true,
                                Amount = 22,
                                CostType = CostType.WATER
                            },
                            new ApartmentCost
                            {
                                Id = 12,
                                ApartmentId = 1,
                                Month = Month.DECEMBER,
                                IsPaid = true,
                                Amount = 22,
                                CostType = CostType.GAS
                            },
                            new ApartmentCost
                            {
                                Id = 13,
                                ApartmentId = 1,
                                Month = Month.SEPTEMBER,
                                IsPaid = true,
                                Amount = 34,
                                CostType = CostType.ELECTRICITY
                            });
        }
    }
}
