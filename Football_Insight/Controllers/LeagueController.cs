using Football_Insight.Data;
using Football_Insight.Data.Enums;
using Football_Insight.Models.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Controllers
{
    public class LeagueController : BaseController
    {
        private readonly FootballInsightDbContext context;
        private readonly ILogger<HomeController> logger;

        public LeagueController(FootballInsightDbContext Context, ILogger<HomeController> Logger) : base(Context)
        {
            logger = Logger;
            context = Context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var teams = await context.Teams
                .Where(t => t.LeagueId == id)
                .Select(t => new TeamTableViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Wins =
                        t.HomeMatches.Where(hm => hm.HomeScore > hm.AwayScore && hm.Status == MatchStatus.Finished).Count() +
                        t.AwayMatches.Where(am => am.AwayScore > am.HomeScore && am.Status == MatchStatus.Finished).Count(),
                    Draws =
                        t.HomeMatches.Where(hm => hm.HomeScore == hm.AwayScore && hm.Status == MatchStatus.Finished).Count() +
                        t.AwayMatches.Where(am => am.AwayScore == am.HomeScore && am.Status == MatchStatus.Finished).Count(),
                    Losses =
                        t.HomeMatches.Where(hm => hm.HomeScore < hm.AwayScore && hm.Status == MatchStatus.Finished).Count() +
                        t.AwayMatches.Where(am => am.AwayScore < am.HomeScore && am.Status == MatchStatus.Finished).Count(),
                    GoalsAgainst =
                        t.AwayMatches.Sum(am => am.HomeScore) +
                        t.HomeMatches.Sum(hm => hm.AwayScore),
                    GoalsFor =
                        t.AwayMatches.Sum(am => am.AwayScore) +
                        t.HomeMatches.Sum(hm => hm.HomeScore),
                    Logo = t.LogoURL,
                    Points =
                        t.HomeMatches.Where(hm => hm.HomeScore > hm.AwayScore && hm.Status == MatchStatus.Finished).Count() * 3 +
                        t.AwayMatches.Where(am => am.AwayScore > am.HomeScore && am.Status == MatchStatus.Finished).Count() * 3 +
                        t.HomeMatches.Where(hm => hm.HomeScore == hm.AwayScore && hm.Status == MatchStatus.Finished).Count() +
                        t.AwayMatches.Where(am => am.AwayScore == am.HomeScore && am.Status == MatchStatus.Finished).Count(),
                })
                .OrderBy(t => t.Points)
                .ToListAsync();

            return View(teams);
        }
    }
}
