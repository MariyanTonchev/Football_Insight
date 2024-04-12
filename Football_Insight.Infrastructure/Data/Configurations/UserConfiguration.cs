using Football_Insight.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Football_Insight.Infrastructure.Constants.DataConstants;

namespace Football_Insight.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser {
                    Id = UserGUID,
                    UserName = "user@fi.com",
                    NormalizedUserName = "USER@FI.COM",
                    Email = "user@fi.com",
                    NormalizedEmail = "USER@FI.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "User123!") 
                },
                new ApplicationUser
                {
                    Id = AdminGUID,
                    UserName = "admin@fi.com",
                    NormalizedUserName = "ADMIN@FI.COM",
                    Email = "admin@fi.com",
                    NormalizedEmail = "ADMIN@FI.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123!")
                }
            );
        }
    }
}
