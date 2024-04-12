using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Football_Insight.Infrastructure.Constants.DataConstants;

namespace Football_Insight.Infrastructure.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole { Id = RoleUserGUID, Name = "User", NormalizedName = "USER" },
                new IdentityRole { Id = RoleAdminGUID, Name = "Admin", NormalizedName = "ADMIN" }
            );
        }
    }
}
