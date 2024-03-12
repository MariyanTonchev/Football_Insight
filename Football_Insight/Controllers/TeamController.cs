using Football_Insight.Data;
using Football_Insight.Data.Enums;
using Football_Insight.Models.Coach;
using Football_Insight.Models.League;
using Football_Insight.Models.Match;
using Football_Insight.Models.Player;
using Football_Insight.Models.Stadium;
using Football_Insight.Models.Team;
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

        public async Task<IActionResult> Index(int Id)
        {
            var tean = await context.Teams
                .Where(t => t.Id == Id)
                .Include(t => t.Coach)
                .Include(t => t.League)
                .Include(t => t.Stadium)
                .FirstOrDefaultAsync();

            var viewModel = new TeamDetailedViewModel
            {
                Name = tean.Name,

                Founded = tean.Founded,
                LogoURL = tean.LogoURL,
                Fixtures = new List<MatchSimpleViewModel> { },
                Results = new List<MatchSimpleViewModel> { },
                League = new LeagueSimpleViewModel
                {
                    Id = tean.League.Id,
                    Name = tean.League.Name,
                },
                Stadium = new StadiumSimpleViewModel
                {
                    Id = tean.Stadium.Id,
                    Name = tean.Stadium.Name
                },
                Coach = new CoachSimpleViewModel
                {
                    Id = tean.Coach.Id,
                    Name = $"{tean.Coach.FirstName} {tean.Coach.LastName}"
                },
                Players = tean.Players
                    .Select(p => new PlayerSimpleViewModel() 
                    { 
                        Id = p.Id,
                        Name = $"{p.FirstName} {p.LastName}"
                    })
                    .ToList()
            };
                
            return View(viewModel);
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
    }
}
