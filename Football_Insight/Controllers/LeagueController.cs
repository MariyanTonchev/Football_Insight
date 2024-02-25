using Football_Insight.Data;
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
                })
                .ToListAsync();

            return View(teams);
        }
    }
}
