using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Match;
using Football_Insight.Core.Models.Team;
using Football_Insight.Core.Services;
using Football_Insight.Infrastructure.Data;
using Football_Insight.Infrastructure.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Controllers
{
    public class LeagueController : BaseController
    {

        private readonly ILogger<HomeController> logger;
        private readonly ILeagueService leagueService;

        public LeagueController(ILogger<HomeController> _logger, ILeagueService _leagueService)
        {
            logger = _logger;
            leagueService = _leagueService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var viewModel = await leagueService.GetLeagueViewDataAsync(id);

            if(leagueService == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var viewModel = await leagueService.GetAllLeaguesAsync();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeagueCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await leagueService.CreateLeagueAsync(model);

                if (result.Success)
                {
                    return RedirectToAction(nameof(Index), new { id = result.LeagueId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int leagueId)
        {
            var league = await leagueService.GetLeagueDetailsAsync(leagueId);

            if(league == null)
            {
                return NotFound();
            }

            return View(league);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LeagueEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {        
                return View(viewModel);
            }

            var updateResult = await leagueService.UpdateLeagueAsync(viewModel);

            if (!updateResult.Success)
            {
                ModelState.AddModelError("", updateResult.Message);
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index), new { id = viewModel.Id });
        }
    }
}
