using Football_Insight.Data.Configurations;
using Football_Insight.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Football_Insight.Data
{
    public class FootballInsightDbContext : IdentityDbContext
    {
        public FootballInsightDbContext(DbContextOptions<FootballInsightDbContext> options)
            : base(options)
        {

        }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PlayerMatch> PlayerMatches { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PlayerMatch>()
                .HasKey(pm => new { pm.PlayerId, pm.MatchId });

            builder.Entity<PlayerMatch>()
                .HasOne(pm => pm.Player)
                .WithMany(p => p.PlayersMatches)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PlayerMatch>()
                .HasOne(pm => pm.Match)
                .WithMany(m => m.PlayersMatches)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PlayerStatistic>()
                .HasOne(ps => ps.Player)
                .WithMany(p => p.PlayerStatistics)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ApplyConfiguration(new LeagueConfiguration());
            builder.ApplyConfiguration(new StadiumConfiguration());
            builder.ApplyConfiguration(new CoachConfiguration());
            builder.ApplyConfiguration(new TeamConfiguration());
            builder.ApplyConfiguration(new PlayerConfiguration());
            builder.ApplyConfiguration(new PositionConfiguration());
        }
    }
}
