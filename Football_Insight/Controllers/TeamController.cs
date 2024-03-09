using Football_Insight.Data;
using Football_Insight.Models.League;
using Football_Insight.Models.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Controllers
{
    public class TeamController : BaseController
    {
        private readonly FootballInsightDbContext context;
        private readonly ILogger<TeamController> logger;

        public TeamController(FootballInsightDbContext Context, ILogger<TeamController> Logger) : base(Context)
        {
            context = Context;
            logger = Logger;
        }

        public async Task<IActionResult> Index()
        {
            var leagues = await context.Leagues
                .Select(l => new LeagueTeamsViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Teams = context.Teams
                        .Where(t => t.LeagueId == l.Id)
                        .Select(t => new SimpleTeamViewModel
                        {
                            Id = t.Id,
                            Name = t.Name,
                        })
                        .ToList()
                })
                .ToListAsync();

            return View(leagues);
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
                            .Select(t => new SimpleTeamViewModel
                            {
                                Id = t.Id,
                                Name = t.Name,
                            })
                            .ToList()
                    })
                    .ToListAsync();

            return View(leagues);
        }
    }
}
