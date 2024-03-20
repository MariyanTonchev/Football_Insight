using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football_Insight.Data.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasData(
                new Team { Id = 1, Name = "Manchester United", Founded = 1878, LeagueId = 1, StadiumId = 1 },
                new Team { Id = 2, Name = "Real Madrid", Founded = 1902, LeagueId = 2, StadiumId = 2 },
                new Team { Id = 3, Name = "FC Bayern Munich", Founded = 1900, LeagueId = 3, StadiumId = 3 },
                new Team { Id = 4, Name = "AC Milan", Founded = 1899, LeagueId = 4, StadiumId = 4 },
                new Team { Id = 5, Name = "Paris Saint-Germain", Founded = 1970, LeagueId = 5, StadiumId = 5 },
                new Team { Id = 6, Name = "Chelsea FC", Founded = 1905, LeagueId = 1, StadiumId = 6 },
                new Team { Id = 7, Name = "FC Barcelona", Founded = 1899, LeagueId = 2, StadiumId = 7 },
                new Team { Id = 8, Name = "Borussia Dortmund", Founded = 1909, LeagueId = 3, StadiumId = 2 },
                new Team { Id = 9, Name = "Inter Milan", Founded = 1908, LeagueId = 4, StadiumId = 4 },
                new Team { Id = 10, Name = "Olympique Lyonnais", Founded = 1950, LeagueId = 5, StadiumId = 3 },
                new Team { Id = 11, Name = "Arsenal", Founded = 1886, LeagueId = 1, StadiumId = 1 }
                );
        }
    }
}
