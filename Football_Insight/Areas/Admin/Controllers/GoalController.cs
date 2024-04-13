using Football_Insight.Controllers;
using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Goal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GoalController : BaseController
    {
        private readonly IGoalService goalService;
        private readonly ILogger<GoalController> logger;

        public GoalController(IGoalService _goalService, ILogger<GoalController> _logger)
        {
            goalService = _goalService;
            logger = _logger;
        }

        [HttpGet]
        public IActionResult Add(int teamId, int matchId)
        {
            try
            {
                if (teamId == 0 || matchId == 0)
                {
                    return NotFound();
                }

                var result = ViewComponent("GoalModalComponent", new { teamId, matchId });

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while trying get goal modal.");
                ModelState.AddModelError("", "An unexpected error occurred.");
                return StatusCode(500);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GoalModalViewModel viewModel)
        {
            try
            {
                var result = await goalService.AddGoalAsync(viewModel);

                TempData["Status"] = result.Success;
                TempData["Message"] = result.Message;

                return RedirectToAction("Index", "Match", new { Area = "User", viewModel.MatchId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while trying to add a goal with teamId {TeamId} and matchId {MatchId}", viewModel.TeamId, viewModel.MatchId);
                ModelState.AddModelError("", "An unexpected error occurred.");
                return View(viewModel);
            }
        }
    }
}
