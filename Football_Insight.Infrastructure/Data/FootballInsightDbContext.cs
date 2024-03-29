using Football_Insight.Data.Configurations;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Infrastructure.Data
{
    public class FootballInsightDbContext : IdentityDbContext<ApplicationUser>
    {
        public FootballInsightDbContext(DbContextOptions<FootballInsightDbContext> options)
            : base(options)
        {

        }
        public DbSet<League> Leagues { get; set; } = null!;
        public DbSet<Match> Matches { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!;
        public DbSet<PlayerMatch> PlayerMatches { get; set; } = null!;
        public DbSet<Stadium> Stadiums { get; set; } = null!;

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
            builder.ApplyConfiguration(new TeamConfiguration());
            builder.ApplyConfiguration(new PlayerConfiguration());
            builder.ApplyConfiguration(new PositionConfiguration());
        }
    }
}
