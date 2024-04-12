using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Team;
using Football_Insight.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Controllers
{
    public class TeamController : BaseController
    {
        private readonly ILeagueService leagueService;
        private readonly ITeamService teamService;
        private readonly IStadiumService stadiumService;
        private readonly ILogger<TeamController> logger;

        public TeamController(ILeagueService _leagueService, 
                                ITeamService _teamService,
                                IStadiumService _stadiumService,
                                ILogger<TeamController> _logger) 
        {
            leagueService = _leagueService;
            teamService = _teamService;
            stadiumService = _stadiumService;
            logger = _logger;
        }

        public IActionResult Index(int teamId)
        {
            if(teamId == 0)
            {
                return BadRequest();
            }

            return View(teamId);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = await teamService.GetCreateFormViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamFormViewModel viewModel)
        {
            var result = await teamService.CreateTeamAsync(viewModel);

            if(!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index), new {TeamId = result.ObjecId});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int teamId)
        {
            var viewModel = await teamService.GetEditFormViewModel(teamId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeamFormViewModel viewModel, int teamId)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            var result = await teamService.UpdateTeamAsync(viewModel, teamId);

            if (!result.Success)
            {
                viewModel.Leagues = await leagueService.GetAllLeaguesAsync();
                viewModel.Stadiums = await stadiumService.GetAllStadiumAsync();
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index), new { teamId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int teamId)
        {
            var viewModel = await teamService.GetTeamSimpleViewModelAsync(teamId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TeamSimpleViewModel viewModel)
        {
            var result = await teamService.DeleteTeamAsync(viewModel.TeamId);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction(nameof(All), new { teamId = viewModel.TeamId });
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
