using Football_Insight.Core.Models;
using Football_Insight.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Football_Insight.Controllers
{

    public class HomeController : BaseController
    {
        private readonly FootballInsightDbContext context;
        private readonly ILogger<HomeController> logger;

        public HomeController(FootballInsightDbContext Context, ILogger<HomeController> Loggerr)
        {
            context = Context;
            logger = Loggerr;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if(User.Identity != null && !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

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
