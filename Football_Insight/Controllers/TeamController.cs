using Football_Insight.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Controllers
{
    public class TeamController : BaseController
    {
        private readonly ILeagueService leagueService;
        private readonly ITeamService teamService;
        private readonly ILogger<TeamController> logger;

        public TeamController(ILeagueService _leagueService, 
                                ITeamService _teamService,  
                                ILogger<TeamController> _logger) 
        {
            leagueService = _leagueService;
            teamService = _teamService;
            logger = _logger;
        }

        public IActionResult Index(int TeamId)
        {
            return View(TeamId);
        }

        public async Task<IActionResult> All()
        {
            var viewModel = await leagueService.GetAllLeaguesWithTeamsAsync();

            return View(viewModel);
        }

        public async Task<IActionResult> Fixtures(int Id)
        {
            var viewModel = await teamService.GetTeamFixturesAsync(Id);

            return View(viewModel);
        }

        public async Task<IActionResult> Results(int Id)
        {
            var viewModel = await teamService.GetTeamResultsAsync(Id);

            return View(viewModel);
        }

        public async Task<IActionResult> Squad(int Id)
        {
            var viewModel = await teamService.GetTeamSquadAsync(Id);

            return View(viewModel);
        }
    }
}
