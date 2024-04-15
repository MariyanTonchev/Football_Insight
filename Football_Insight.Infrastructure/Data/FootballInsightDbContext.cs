using Football_Insight.Data.Configurations;
using Football_Insight.Infrastructure.Data.Configurations;
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
        public DbSet<Stadium> Stadiums { get; set; } = null!;
        public DbSet<Goal> Goals { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Player>()
                .HasMany(p => p.GoalsScored)
                .WithOne(g => g.GoalScorer)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Player>()
                .HasMany(p => p.GoalsAssisted)
                .WithOne(g => g.GoalAssistant)
                .HasForeignKey(g => g.GoalAssistantId)
                .OnDelete(DeleteBehavior.NoAction); 

            builder.Entity<Team>()
                .HasMany(t => t.Goals)
                .WithOne(g => g.Team)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(uf => uf.Favorites);

            builder.Entity<Goal>()
                .Property(g => g.GoalAssistantId)
                .IsRequired(false);

            builder.Entity<Favorite>()
                .HasOne(f => f.Match)
                .WithMany(fm => fm.Favorites);

            builder.Entity<Favorite>()
                .HasKey(f => new { f.UserId, f.MatchId });


            builder.ApplyConfiguration(new LeagueConfiguration());
            builder.ApplyConfiguration(new StadiumConfiguration());
            builder.ApplyConfiguration(new TeamConfiguration());
            builder.ApplyConfiguration(new PlayerConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new MatchConfiguration());
            builder.ApplyConfiguration(new GoalConfiguration());
        }
    }
}
