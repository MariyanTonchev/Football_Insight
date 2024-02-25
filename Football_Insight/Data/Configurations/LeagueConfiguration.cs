using Football_Insight.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football_Insight.Data.Configurations
{
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasData(
                new League { Id = 1, Name = "Premier League" },
                new League { Id = 2, Name = "La Liga" },
                new League { Id = 3, Name = "Bundesliga" },
                new League { Id = 4, Name = "Serie A" },
                new League { Id = 5, Name = "Ligue 1" }
            );
        }
    }
}
