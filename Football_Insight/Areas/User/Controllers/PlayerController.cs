using Football_Insight.Controllers;
using Football_Insight.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = ("User"))]
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
        public async Task<IActionResult> Index(int playerId)
        {
            if(playerId <= 0)
            {
                logger.LogWarning("Attempted to access Player Index with invalid ID: {PlayerId}", playerId);
                return BadRequest();
            }

            try
            {
                var viewModel = await playerService.GetPlayerDetailsAsync(playerId);

                if (viewModel == null)
                {
                    return NotFound();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve details for player ID {PlayerId}", playerId);
                return StatusCode(500);
            }
        }
    }
}
