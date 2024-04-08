using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Goal;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Controllers
{
    public class GoalController : BaseController
    {
        private readonly IGoalService goalService;

        public GoalController(IGoalService _goalService)
        {
            goalService = _goalService;
        }

        [HttpGet]
        public IActionResult Add(int teamId, int matchId)
        {
            var result = ViewComponent("GoalModalComponent", new { teamId = teamId, matchId = matchId });

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GoalModalViewModel viewModel)
        {
            var result = await goalService.AddGoalAsync(viewModel);

            if (!result.Success) 
            {
                TempData["Status"] = result.Success;
                TempData["Message"] = result.Message;
                ModelState.AddModelError("", result.Message);
                return RedirectToAction("Index", "Match", new { matchId = viewModel.MatchId });
            }

            TempData["Status"] = result.Success;
            TempData["Message"] = result.Message;
            return RedirectToAction("Index", "Match", new { matchId = viewModel.MatchId });
        }
    }
}
