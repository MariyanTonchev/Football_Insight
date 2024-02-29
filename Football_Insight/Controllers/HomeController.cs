using Football_Insight.Data;
using Football_Insight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Football_Insight.Controllers
{

    public class HomeController : BaseController
    {
        private readonly FootballInsightDbContext context;
        private readonly ILogger<HomeController> logger;

        public HomeController(FootballInsightDbContext Context, ILogger<HomeController> Loggerr) : base(Context)
        {
            context = Context;
            logger = Loggerr;
        }

        public async Task<IActionResult> Index()
        {
            var leagues = await context.Leagues
                            .Select(l => new
                            {
                                l.Id,
                                l.Name,
                            })
                            .ToListAsync();

            ViewData["Leagues"] = leagues;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
