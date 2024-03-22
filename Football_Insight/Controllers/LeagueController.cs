using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Match;
using Football_Insight.Core.Models.Team;
using Football_Insight.Infrastructure.Data;
using Football_Insight.Infrastructure.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Controllers
{
    public class LeagueController : BaseController
    {

        private readonly ILogger<HomeController> logger;
        private readonly ILeagueService leagueService;

        public LeagueController(ILogger<HomeController> _logger, ILeagueService _leagueService)
        {
            logger = _logger;
            leagueService = _leagueService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var viewModel = await leagueService.GetLeagueViewDataAsync(id);

            return View(viewModel);
        }
    }
}
