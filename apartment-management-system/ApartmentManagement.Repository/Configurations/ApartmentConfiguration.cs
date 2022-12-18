using ApartmentManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentManagement.Repository.Configurations
{
    internal class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ApartmentNumber).IsRequired();
            builder.Property(x => x.BlockNumber).IsRequired();
            builder.Property(x => x.Floor).IsRequired();
            builder.Property(x => x.Status).IsRequired().HasConversion<string>();
            builder.Property(x => x.Type).IsRequired();
            builder.HasOne(x => x.User).WithOne(x => x.Apartment).HasForeignKey<Apartment>(x => x.UserId).IsRequired(false);
        }
    }
}
