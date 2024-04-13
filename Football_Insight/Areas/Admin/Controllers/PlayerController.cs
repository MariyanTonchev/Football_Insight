using Football_Insight.Controllers;
using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Player;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PlayerController : BaseController
    {
        private readonly IPlayerService playerService;
        private readonly ILogger<PlayerController> logger;

        public PlayerController(IPlayerService _playerService, ILogger<PlayerController> _logger)
        {
            playerService = _playerService;
            logger = _logger;
        }

        [HttpGet]
        public IActionResult Create(int teamId)
        {
            try
            {
                var viewModel = playerService.GetCreateFomViewModel(teamId);
                if (viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve create form for team ID {TeamId}", teamId);
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var result = await playerService.CreatePlayerAsync(viewModel);

                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(viewModel);
                }

                return RedirectToAction("Squad", "Team", new { Area = "User", Id = viewModel.TeamId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to create player");
                ModelState.AddModelError("", "An unexpected error occurred while creating the player.");
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int playerId)
        {
            if (playerId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var viewModel = await playerService.GetEditFormViewModel(playerId);
                if (viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve edit form for player ID {PlayerId}", playerId);
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlayerFormViewModel viewModel, int playerId)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                viewModel.Positions = playerService.GetPositionsFromEnum();
                return View(viewModel);
            }
            try
            {
                var result = await playerService.UpdatePlayerAsync(viewModel, playerId);

                if (!result.Success)
                {
                    viewModel.Positions = playerService.GetPositionsFromEnum();
                    ModelState.AddModelError("", result.Message);
                    return View(viewModel);
                }

                return RedirectToAction("Squad", "Team", new { Area = "User", Id = viewModel.TeamId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating player with ID {PlayerId}", playerId);
                ModelState.AddModelError("", "An unexpected error occurred while trying to update the player.");
                return View(viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int playerId)
        {
            if (playerId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var viewModel = await playerService.GetPlayerSimpleViewModelAsync(playerId);
                if (viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve player details for deletion with ID {PlayerId}", playerId);
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PlayerSimpleViewModel viewModel)
        {
            try
            {
                var result = await playerService.DeletePlayerAsync(viewModel.PlayerId);

                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(viewModel);
                }

                return RedirectToAction("Squad", "Team", new { Area = "User", Id = viewModel.TeamId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting player with ID {PlayerId}", viewModel.PlayerId);
                ModelState.AddModelError("", "An unexpected error occurred while trying to delete the player.");
                return View(viewModel);
            }
        }
    }
}
