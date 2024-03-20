using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Match;
using Football_Insight.Core.Models.Player;
using Football_Insight.Core.Models.Team;
using Football_Insight.Infrastructure.Data;
using Football_Insight.Infrastructure.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Controllers
{
    public class TeamController : BaseController
    {
        private readonly FootballInsightDbContext context;
        private readonly ILogger<TeamController> logger;

        public TeamController(FootballInsightDbContext Context, ILogger<TeamController> Logger) 
        {
            context = Context;
            logger = Logger;
        }

        public IActionResult Index(int TeamId)
        {
            return View(TeamId);
        }

        public async Task<IActionResult> All()
        {
            var leagues = await context.Leagues
                    .Select(l => new LeagueTeamsViewModel
                    {
                        Id = l.Id,
                        Name = l.Name,
                        Teams = context.Teams
                            .Where(t => t.LeagueId == l.Id)
                            .Select(t => new TeamSimpleViewModel
                            {
                                Id = t.Id,
                                Name = t.Name,
                            })
                            .ToList()
                    })
                    .ToListAsync();

            return View(leagues);
        }

        public async Task<IActionResult> Fixtures(int Id)
        {
            var matches = await context.Matches
                .Include(t => t.HomeTeam)
                .Include(t => t.AwayTeam)
                .Where(m => (m.HomeTeamId == Id || m.AwayTeamId == Id) && (m.Status == MatchStatus.Scheduled)) 
                .Select(m => new MatchFixtureViewModel
                {
                    Id = m.Id,
                    HomeTeam = m.HomeTeam.Name,
                    AwayTeam = m.AwayTeam.Name,
                    Date = m.Date.ToString()
                })
                .ToListAsync();

            var teamFixtures = new TeamFixturesViewModel
            {
                TeamId = Id,
                Fixtures = matches
            };

            return View(teamFixtures);
        }

        public async Task<IActionResult> Results(int Id)
        {
            var matches = await context.Matches
                .Include(t => t.HomeTeam)
                .Include(t => t.AwayTeam)
                .Where(m => (m.HomeTeamId == Id || m.AwayTeamId == Id) && (m.Status == MatchStatus.Finished))
                .Select(m => new MatchResultViewModel
                {
                    Id = m.Id,
                    HomeTeam = m.HomeTeam.Name,
                    AwayTeam = m.AwayTeam.Name,
                    HomeTeamGoals = m.HomeScore,
                    AwayTeamGoals = m.AwayScore,
                    Date = m.Date.ToString()
                })
                .ToListAsync();

            var teamFixtures = new TeamResultsViewModel
            {
                TeamId = Id,
                Results = matches
            };

            return View(teamFixtures);
        }

        public async Task<IActionResult> Squad(int Id)
        {
            var players = await context.Players
                .Where(p => p.TeamId == Id)
                .Select(p => new PlayerSquadViewModel
                {

                })
                .ToListAsync();

            var teamSquad = new TeamSquadViewModel
            {
                TeamId = Id,
                Players = players
            };

            return View(teamSquad);
        }
    }
}
