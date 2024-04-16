using Football_Insight.Controllers;
using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Match;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace Football_Insight.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ("Admin"))]
    public class MatchController : BaseController
    {
        private readonly IMatchService matchService;
        private readonly ILeagueService leagueService;
        private readonly ISchedulerFactory schedulerFactory;
        private readonly IMatchTimerService matchTimerService;
        private readonly ILogger<MatchController> logger;

        public MatchController(IMatchService _matchService, 
                        ILeagueService _leagueService, 
                        ISchedulerFactory _schedulerFactory, 
                        ILogger<MatchController> _logger, 
                        IMatchTimerService _matchTimerService)
        {
            matchService = _matchService;
            leagueService = _leagueService;
            schedulerFactory = _schedulerFactory;
            logger = _logger;
            matchTimerService = _matchTimerService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int leagueId)
        {
            try
            {
                var teams = await leagueService.GetAllTeamsAsync(leagueId);

                if (teams == null || !teams.Any()) 
                {
                    TempData["ErrorMessage"] = "No teams available for this league.";
                    return RedirectToAction("Index", "League", new {Area = "User", Id = leagueId});
                }

                var viewModel = new MatchFormViewModel()
                {
                    LeagueId = leagueId,
                    Teams = teams
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while preparing the creation match form.");
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MatchFormViewModel viewModel, int leagueId)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Teams = await leagueService.GetAllTeamsAsync(leagueId);
                return View(viewModel);
            }

            try
            {
                await matchService.CreateMatchAsync(viewModel, leagueId);
                return RedirectToAction("Index", "League", new { Area = "User", id = leagueId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating match ([{HomeTeamId}] - [{AwayTeamId}]AwayTeamName) for league ID {LeagueId}", leagueId, viewModel.HomeTeamId, viewModel.AwayTeamId);
                ModelState.AddModelError("", "An error occurred while trying to create the match.");
                viewModel.Teams = await leagueService.GetAllTeamsAsync(leagueId);
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int matchId)
        {
            var match = await matchService.GetMatchFormViewModelByIdAsync(matchId);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MatchFormViewModel viewModel, int matchId)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Teams = await leagueService.GetAllTeamsAsync(viewModel.LeagueId);
                return View(viewModel);
            }

            try
            {
                var result = await matchService.UpdateMatchAsync(viewModel, matchId);

                if (!result.Success)
                {
                    viewModel.Teams = await leagueService.GetAllTeamsAsync(viewModel.LeagueId);
                    ModelState.AddModelError("", result.Message);
                    return View(viewModel);
                }

                return RedirectToAction("Index", "Match", new { Area = "User", matchId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating match with ID {MatchId}", matchId);
                ModelState.AddModelError("", "An unexpected error occurred while updating the match.");
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Start(int matchId)
        {
            if (matchId <= 0)
            {
                logger.LogError("Invalid match ID {MatchId} provided", matchId);
                return BadRequest();
            }

            try
            {
                var viewModel = await matchService.GetMatchSimpleViewAsync(matchId);

                if (viewModel == null)
                {
                    logger.LogWarning("No match found with ID {MatchId}", matchId);
                    return NotFound();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving match with ID {MatchId}", matchId);
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Start(MatchSimpleViewModel viewModel)
        {
            if (viewModel == null || viewModel.MatchId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var result = await matchService.StartMatchAsync(viewModel.MatchId);
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(viewModel);
                }
                return RedirectToAction("Index", "Match", new { Area = "User", viewModel.MatchId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred while starting the match.");
                logger.LogError(ex, "Error starting match with ID {MatchId}", viewModel.MatchId);
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int matchId)
        {
            try
            {
                var match = await matchService.GetMatchSimpleViewAsync(matchId);
                if (match == null)
                {
                    return NotFound();
                }
                return View(match);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve match with ID {MatchId}", matchId);
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(MatchSimpleViewModel model)
        {
            try
            {
                var result = await matchService.DeleteMatchAsync(model.MatchId);

                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }

                return RedirectToAction("Index", "League", new { Area = "User", id = model.LeagueId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred while deleting the match.");
                logger.LogError(ex, "Error deleting match with ID {MatchId}", model.MatchId);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult GetMatchMinute(int matchId)
        {
            try
            {
                int? minutes = matchTimerService.GetMatchMinute(matchId);
                if (!minutes.HasValue)
                {
                    return NotFound($"No match found with ID {matchId}.");
                }

                var data = new
                {
                    Minutes = minutes.Value
                };

                return Json(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get match minutes for match ID {MatchId}", matchId);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Pause(int matchId)
        {
            try
            {
                var match = await matchService.GetMatchSimpleViewAsync(matchId);

                if (match == null)
                {
                    return NotFound();
                }

                return View(match);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve match details for match ID {MatchId}", matchId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Pause(MatchSimpleViewModel viewModel)
        {
            if (viewModel == null || viewModel.MatchId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var result = await matchService.PauseMatchAsync(viewModel.MatchId);

                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(viewModel);
                }

                return RedirectToAction("Index", "Match", new { Area = "User", viewModel.MatchId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred while trying to pause the match.");
                logger.LogError(ex, "Error pausing match with ID {MatchId}", viewModel.MatchId);
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Unpause(int matchId)
        {
            if (matchId <= 0)
            {
                logger.LogWarning("Attempted to unpause a match with an invalid ID: {MatchId}", matchId);
                return BadRequest();
            }

            try
            {
                var match = await matchService.GetMatchSimpleViewAsync(matchId);
                if (match == null)
                {
                    return NotFound();
                }
                return View(match);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving match with ID {MatchId}", matchId);
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Unpause(MatchSimpleViewModel viewModel)
        {
            if (viewModel == null || viewModel.MatchId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var result = await matchService.UnpauseMatchAsync(viewModel.MatchId);

                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(viewModel);
                }

                return RedirectToAction("Index", "Match", new { Area = "User", viewModel.MatchId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred while trying to unpause the match.");
                logger.LogError(ex, "Error unpausing match with ID {MatchId}", viewModel.MatchId);
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> End(int matchId)
        {
            try
            {
                var match = await matchService.GetMatchEndViewAsync(matchId);

                if (match == null)
                {
                    return NotFound();
                }

                return View(match);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve match details for ending the match with ID {MatchId}", matchId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> End(MatchSimpleViewModel viewModel)
        {
            if (viewModel == null || viewModel.MatchId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var result = await matchService.EndMatchAsync(viewModel.MatchId);

                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(viewModel);
                }

                return RedirectToAction("Index", "Match", new { Area = "User", viewModel.MatchId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error ending match with ID {MatchId}", viewModel.MatchId);
                ModelState.AddModelError("", "An unexpected error occurred while trying to end the match.");
                return View(viewModel);
            }
        }
    }
}
