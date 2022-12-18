using ApartmentManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentManagement.Repository.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Status).IsRequired().HasConversion<string>();
            builder.HasOne(x => x.User).WithMany(x => x.Message).HasForeignKey(x => x.UserId);
        }
    }
}
