using Football_Insight.Controllers;
using Football_Insight.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = ("User"))]
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

        [HttpGet]
        public IActionResult Index(int teamId)
        {
            if(teamId <= 0)
            {
                return BadRequest();
            }

            return View(teamId);
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var viewModel = await leagueService.GetAllLeaguesWithTeamsAsync();
                if(viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving all leagues with teams");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Fixtures(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var viewModel = await teamService.GetTeamFixturesAsync(Id);
                if (viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load fixtures for team ID {Id}", Id);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Results(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var viewModel = await teamService.GetTeamResultsAsync(Id);
                if (viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load results for team ID {Id}", Id);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Squad(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var viewModel = await teamService.GetTeamSquadAsync(Id);
                if (viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load squad for team ID {Id}", Id);
                return StatusCode(500);
            }
        }
    }
}
