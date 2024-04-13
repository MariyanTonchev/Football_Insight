using Football_Insight.Controllers;
using Football_Insight.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
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
        public async Task<IActionResult> Index(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var viewModel = await leagueService.GetLeagueViewDataAsync(id);
                if (viewModel == null)
                {
                    return NotFound();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to load league details for ID {Id}", id);
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var viewModel = await leagueService.GetAllLeaguesAsync();
                if (viewModel == null)
                {
                    return NotFound();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching all leagues");
                return StatusCode(500);
            }
        }
    }
}
