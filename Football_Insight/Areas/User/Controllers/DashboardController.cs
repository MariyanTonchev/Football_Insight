using Football_Insight.Controllers;
using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Football_Insight.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class DashboardController : BaseController
    {
        private readonly IDashboardService dashboardService;
        private readonly ILogger<DashboardController> logger;

        public DashboardController(ILogger<DashboardController> Loggerr, IDashboardService _dashboardService)
        {
            logger = Loggerr;
            dashboardService = _dashboardService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.Identity != null && !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = await dashboardService.GetDashboardViewModelAsync();

            return View(viewModel);
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
