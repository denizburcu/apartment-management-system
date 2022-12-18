using ApartmentManagement.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentManagement.Repository.Seeds
{
    internal class MessageSeed : IEntityTypeConfiguration<Message>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Message> builder)
        {
                builder.HasData(new Message
                {
                    Id = 1,
                    Description = "Apartman temizlenmemiş",
                    UserId = "02174cf0–9412–4cfe-afbf-53422d33cf6",
                    Status = MessageStatus.NEW

                },
                new Message
                {
                    Id = 2,
                    Description = "Faturaları ödedim",
                    UserId = "02174cf0–9412–4cfe-afbf-53422d33cf6",
                    Status = MessageStatus.NEW

                },
                new Message
                {
                    Id = 3,
                    Description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis",
                    UserId = "02174cf0–9412–4cfe-afbf-5fhdf6d33cf6",
                    Status = MessageStatus.NEW

                },
                new Message
                {
                    Id = 4,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                    UserId = "02174cf0–9412–4cfe-afbf-5fhdf6d33cf6",
                    Status = MessageStatus.NEW

                },
                new Message
                {
                    Id = 5,
                    Description = " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ",
                    UserId = "02174cf0–9412–4cfe-afbf-591231sd6d33cf6",
                    Status = MessageStatus.NEW

                },
                new Message
                {
                    Id = 6,
                    Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                    UserId = "02174cf0–9412–4cfe-afbf-591231sd6d33cf6",
                    Status = MessageStatus.NEW

                },
                new Message
                {
                    Id = 7,
                    Description = "Apartmanda kapıya ayakkabı bırakılmasın..",
                    UserId = "02174cf0–9123xccfe-afbf-59f706d33cf6",
                    Status = MessageStatus.NEW

                },
                new Message
                {
                    Id = 8,
                    Description = "Aidatı ödedim",
                    UserId = "02174cf0–xcvds2e-afbf-59f706d33cf6",
                    Status = MessageStatus.NEW

                });
        }
    }
}
