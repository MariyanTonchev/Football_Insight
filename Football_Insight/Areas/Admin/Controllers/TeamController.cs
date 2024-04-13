using Football_Insight.Controllers;
using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> Create()
        {
            try
            {
                var viewModel = await teamService.GetCreateFormViewModel();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load the team creation form.");
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamFormViewModel viewModel)
        {
            try
            {
                var result = await teamService.CreateTeamAsync(viewModel);
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(viewModel);
                }

                return RedirectToAction("Index", "Team", new { Area = "User", TeamId = result.ObjectId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating a team");
                ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int teamId)
        {
            try
            {
                var viewModel = await teamService.GetEditFormViewModel(teamId);
                if (viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load the edit form for team ID {TeamId}", teamId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeamFormViewModel viewModel, int teamId)
        {
            try
            {
                var result = await teamService.UpdateTeamAsync(viewModel, teamId);

                if (!result.Success)
                {
                    viewModel.Leagues = await leagueService.GetAllLeaguesAsync();
                    viewModel.Stadiums = await stadiumService.GetAllStadiumAsync();
                    ModelState.AddModelError("", result.Message);
                    return View(viewModel);
                }

                return RedirectToAction("Index", "Team", new { Area = "User", teamId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating team with ID {TeamId}", teamId);
                ModelState.AddModelError("", "An unexpected error occurred while updating the team.");
                viewModel.Leagues = await leagueService.GetAllLeaguesAsync();
                viewModel.Stadiums = await stadiumService.GetAllStadiumAsync();
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int teamId)
        {
            try
            {
                var viewModel = await teamService.GetTeamSimpleViewModelAsync(teamId);
                if (viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load team details for deletion with ID {TeamId}", teamId);
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TeamSimpleViewModel viewModel)
        {
            try
            {
                var result = await teamService.DeleteTeamAsync(viewModel.TeamId);

                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(viewModel);
                }

                return RedirectToAction("All", "Team", new { Area = "User", teamId = viewModel.TeamId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting team with ID {TeamId}", viewModel.TeamId);
                ModelState.AddModelError("", "An unexpected error occurred while trying to delete the team.");
                return View(viewModel);
            }
        }
    }
}
