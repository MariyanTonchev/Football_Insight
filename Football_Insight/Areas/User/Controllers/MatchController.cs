using Football_Insight.Controllers;
using Football_Insight.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Areas.User.Controllers
{
    [Area("User")]
    public class MatchController : BaseController
    {
        private readonly IMatchService matchService;
        private readonly ILogger<MatchController> logger;

        public MatchController(IMatchService _matchService, 
                        ILogger<MatchController> _logger, 
                        IMatchTimerService _matchTimerService)
        {
            matchService = _matchService;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int matchId)
        {
            if (matchId <= 0)
            {
                return BadRequest();
            }

            try
            {
                var viewModel = await matchService.GetMatchDetailsAsync(matchId);
                if (viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load match details for Match ID {MatchId}", matchId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFavorite(int matchId)
        {
            try
            {
                var result = await matchService.AddFavoriteAsync(matchId);
                var leagueId = (await matchService.GetMatchSimpleViewAsync(matchId)).LeagueId;

                return RedirectToAction("Index", "League", new { Area = "User", Id = leagueId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding match to favorites: Match ID {MatchId}", matchId);
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFavorite(int matchId)
        {
            try
            {
                var result = await matchService.RemoveFavoriteAsync(matchId);
                var leagueId = (await matchService.GetMatchSimpleViewAsync(matchId)).LeagueId;

                if (!result.Success)
                {
                    ModelState.AddModelError("", result.Message);
                }

                return RedirectToAction("Index", "League", new { Area = "User", Id = leagueId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error removing match to favorites: Match ID {MatchId}", matchId);
                return StatusCode(500);
            }
        }
    }
}
