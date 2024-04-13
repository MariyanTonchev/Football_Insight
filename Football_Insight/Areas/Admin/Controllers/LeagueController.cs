using Football_Insight.Controllers;
using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.League;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LeagueController : BaseController
    {

        private readonly ILogger<LeagueController> logger;
        private readonly ILeagueService leagueService;

        public LeagueController(ILogger<LeagueController> _logger, ILeagueService _leagueService)
        {
            logger = _logger;
            leagueService = _leagueService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while preparing the creation form.");
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeagueFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await leagueService.CreateLeagueAsync(model);
                if (!result.Success)
                {
                    logger.LogError("Failed to create league: {Message}", result.Message);
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View(model);
                }

                return RedirectToAction("Index", "League", new { Area = "User", id = result.ObjectId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating the league.");
                ModelState.AddModelError("", "An unexpected error occurred.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int leagueId)
        {
            try
            {
                if (leagueId <= 0)
                {
                    logger.LogWarning("Invalid league ID {LeagueId} provided for editing.", leagueId);
                    return BadRequest();
                }

                var league = await leagueService.GetLeagueDetailsAsync(leagueId);
                if (league == null)
                {
                    logger.LogWarning("Attempt to edit non-existing league with ID {LeagueId}", leagueId);
                    return NotFound();
                }

                logger.LogInformation("League with ID {LeagueId} retrieved for editing.", leagueId);
                return View(league);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving edit form for league with ID {LeagueId}", leagueId);
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LeagueFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {        
                return View(viewModel);
            }

            try
            {
                var updateResult = await leagueService.UpdateLeagueAsync(viewModel);

                if (!updateResult.Success)
                {
                    ModelState.AddModelError("", updateResult.Message);
                    return View(viewModel);
                }

                logger.LogInformation("League with ID {LeagueId} was edited successfully.", viewModel.Id);
                return RedirectToAction("Index", "League", new { Area = "User", id = viewModel.Id });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update league with ID {Id}", viewModel.Id);
                ModelState.AddModelError("", "An unexpected error occurred while updating the league.");
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int leagueId)
        {
            try
            {
                var league = await leagueService.FindLeagueAsync(leagueId);
                if (league == null)
                {
                    logger.LogWarning("Attempt to delete non-existing league with ID {LeagueId}", leagueId);
                    return NotFound();
                }

                logger.LogInformation("League with ID {LeagueId} retrieved for deletion.", leagueId);
                return View(league);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving league with ID {LeagueId} for deletion", leagueId);
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(LeagueSimpleViewModel model)
        {
            if (model.Id <= 0)
            {
                logger.LogError("Attempted to delete a league with invalid ID {LeagueId}", model.Id);
                return BadRequest();
            }

            try
            {
                var result = await leagueService.DeleteLeagueAsync(model.Id);

                if (!result.Success)
                {
                    logger.LogWarning("Failed to delete league with ID {LeagueId}: {Message}", model.Id, result.Message);
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }

                logger.LogInformation("League with ID {LeagueId} was deleted successfully.", model.Id);

                return RedirectToAction("All", "League", new { Area = "User" });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception occurred while deleting league with ID {LeagueId}", model.Id);
                return StatusCode(500);
            }
        }
    }
}
