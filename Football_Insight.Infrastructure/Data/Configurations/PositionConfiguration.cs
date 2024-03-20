using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Data.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasData(
                new Position { Id = 1, Name = "Goalkeeper" },
                new Position { Id = 2, Name = "Defender" },
                new Position { Id = 3, Name = "Midfielder" },
                new Position { Id = 4, Name = "Forward" }
            );
        }
    }
}
