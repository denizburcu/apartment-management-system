using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApartmentManagement.Repository.Seeds
{
    internal class IdentityUserRoleSeed : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            
            builder.HasData(new IdentityUserRole<string>
            {
                RoleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
                UserId = "02174cf0–9412–4cfe-afbf-59f706d72cf6"
            },
            new IdentityUserRole<string>
            {
                RoleId = "34213123xxx0-asd2–42de-afas29k3X72cf6",
                UserId = "02174cf0–9412–4cfe-afbf-53422d33cf6"
            },
            new IdentityUserRole<string>
            {
                RoleId = "34213123xxx0-asd2–42de-afas29k3X72cf6",
                UserId = "02174cf0–9412–4cfe-afbf-5fhdf6d33cf6"
            },
            new IdentityUserRole<string>
            {
                RoleId = "34213123xxx0-asd2–42de-afas29k3X72cf6",
                UserId = "02174cf0–9412–4cfe-afbf-591231sd6d33cf6"
            },
            new IdentityUserRole<string>
            {
                RoleId = "34213123xxx0-asd2–42de-afas29k3X72cf6",
                UserId = "02174cf0–9123xccfe-afbf-59f706d33cf6"
            },
            new IdentityUserRole<string>
            {
                RoleId = "34213123xxx0-asd2–42de-afas29k3X72cf6",
                UserId = "02174cf0–9cvbcds2-afbf-59f706d33cf6"
            },
            new IdentityUserRole<string>
            {
                RoleId = "34213123xxx0-asd2–42de-afas29k3X72cf6",
                UserId = "02174cf0–xcvds2e-afbf-59f706d33cf6"
            }
            );
        }
    }
}
