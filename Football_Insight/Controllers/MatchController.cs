using Football_Insight.Data;
using Football_Insight.Models.Match;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Controllers
{
    public class MatchController : BaseController
    {
        private readonly FootballInsightDbContext context;
        private readonly ILogger<MatchController> logger;

        public MatchController(FootballInsightDbContext Context, ILogger<MatchController> Logger) : base(Context)
        {
            logger = Logger;
            context = Context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateMatchViewModel();
            return View(viewModel);
        }
    }
}
