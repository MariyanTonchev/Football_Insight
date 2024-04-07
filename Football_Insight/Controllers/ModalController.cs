using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Controllers
{
    public class ModalController : Controller
    {
        public IActionResult GoalModal(int teamId)
        {
            var result = ViewComponent("GoalModal", new { teamId = teamId });

            return result;
        }
    }
}
