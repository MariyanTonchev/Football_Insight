using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football_Insight.Data.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            var players = new List<Player>
        {
            // Team 1 - Manchester United
            new Player { Id = 1, FirstName = "Harry", LastName = "Maguire", DateOfBirth = new DateTime(1993, 3, 5), Position = (int)PlayerPosition.Defender, TeamId = 1 },
            new Player { Id = 2, FirstName = "Marcus", LastName = "Rashford", DateOfBirth = new DateTime(1996, 1, 1), Position = (int)PlayerPosition.Forward, TeamId = 1 },
            new Player { Id = 3, FirstName = "Andre", LastName = "Onana", DateOfBirth = new DateTime(1995, 11, 7), Position = (int)PlayerPosition.Goalkeeper, TeamId = 1 },
            // Continue adding 3 players for each team...
            // Team 11 - Arsenal
            new Player { Id = 31, FirstName = "Bukayo", LastName = "Saka", DateOfBirth = new DateTime(2000, 1, 3), Position = (int)PlayerPosition.Forward, TeamId = 11 },
            new Player { Id = 32, FirstName = "William", LastName = "Salliba", DateOfBirth = new DateTime(2000, 1, 1), Position = (int)PlayerPosition.Defender, TeamId = 11 },
            new Player { Id = 33, FirstName = "Ben", LastName = "White", DateOfBirth = new DateTime(1998, 11, 3), Position = (int)PlayerPosition.Defender, TeamId = 11 },
        };

            builder.HasData(players);
        }
    }
}
