using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Football_Insight.Infrastructure.Constants.DataConstants;

namespace Football_Insight.Infrastructure.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                    new IdentityUserRole<string> { RoleId = RoleUserGUID, UserId = UserGUID },
                    new IdentityUserRole<string> { RoleId = RoleAdminGUID, UserId = AdminGUID },
                    new IdentityUserRole<string> { RoleId = RoleUserGUID, UserId = AdminGUID}
                );
        }
    }
}
