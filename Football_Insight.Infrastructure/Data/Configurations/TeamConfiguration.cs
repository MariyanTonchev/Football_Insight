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
                new Team { Id = 1, Name = "Manchester United", Coach = "Ten Hag",Founded = 1878, LeagueId = 1, StadiumId = 1, LogoURL = "https://static.flashscore.com/res/image/data/nwSRlyWg-h2pPXz3k.png" },
                new Team { Id = 2, Name = "Real Madrid", Coach = "Anceloti", Founded = 1902, LeagueId = 1, StadiumId = 2, LogoURL = "https://static.flashscore.com/res/image/data/A7kHoxZA-fcDVLdrL.png" },
                new Team { Id = 3, Name = "FC Bayern Munich", Coach = "Thomas Tuchel", Founded = 1900, LeagueId = 1, StadiumId = 3, LogoURL = "https://static.flashscore.com/res/image/data/tMir8iCr-xQOIafxi.png" },
                new Team { Id = 4, Name = "AC Milan", Coach = "Sari", Founded = 1899, LeagueId = 1, StadiumId = 4, LogoURL = "https://static.flashscore.com/res/image/data/htYjIjFa-fL3dmxxd.png" },
                new Team { Id = 5, Name = "Paris Saint-Germain", Coach = "Luis Enrique", Founded = 1970, LeagueId = 1, StadiumId = 5, LogoURL ="https://static.flashscore.com/res/image/data/EskJufg5-06Ua3sOf.png" },
                new Team { Id = 6, Name = "Chelsea FC", Coach = "Pochetino", Founded = 1905, LeagueId = 1, StadiumId = 6, LogoURL = "https://static.flashscore.com/res/image/data/GMmvDEdM-IROrZEJb.png" },
                new Team { Id = 7, Name = "FC Barcelona", Coach = "Xavi", Founded = 1899, LeagueId = 1, StadiumId = 7, LogoURL = "https://static.flashscore.com/res/image/data/8dhw5vxS-fcDVLdrL.png" },
                new Team { Id = 8, Name = "Borussia Dortmund", Coach = "Edin Terzić", Founded = 1909, LeagueId = 1, StadiumId = 8, LogoURL = "https://static.flashscore.com/res/image/data/Yiq1eU9r-WrjrBuU2.png" },
                new Team { Id = 9, Name = "Arsenal", Coach = "Mikel Arteta", Founded = 1886, LeagueId = 1, StadiumId = 9 , LogoURL = "https://static.flashscore.com/res/image/data/pfchdCg5-vcNAdtF9.png" },
                new Team { Id = 10, Name = "Liverpool", Coach = "Jurgen Klopp", Founded = 1950, LeagueId = 1, StadiumId = 10, LogoURL = "https://static.flashscore.com/res/image/data/vwC9RGhl-Imx2oQd8.png" }
                );
        }
    }
}
