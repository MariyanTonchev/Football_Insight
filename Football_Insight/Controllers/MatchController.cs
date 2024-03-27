using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Match;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Controllers
{
    public class MatchController : BaseController
    {
        private readonly IMatchService matchService;
        private readonly ILeagueService leagueService;
        private readonly ILogger<MatchController> logger;

        public MatchController(IMatchService _matchService, ILeagueService _leagueService, ILogger<MatchController> _logger)
        {
            matchService = _matchService;
            leagueService = _leagueService;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int leagueId)
        {
            var viewModel = new MatchCreateViewModel()
            {
                Teams = await leagueService.GetAllTeamsAsync(leagueId),
                LeagueId = leagueId
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MatchCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teams = await leagueService.GetAllTeamsAsync(model.LeagueId);
                return View(model);
            }

            var result = await matchService.CreateMatchAsync(model);

            if(!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                model.Teams = await leagueService.GetAllTeamsAsync(model.LeagueId);

                return View(model);
            }

            return RedirectToAction("Index", "League", new { id = model.LeagueId});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int matchId)
        {
            var match = await matchService.GetMatchDetailsAsync(matchId);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MatchEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var updateResult = await matchService.UpdateMatchAsync(viewModel);

            if (!updateResult.Success)
            {
                ModelState.AddModelError("", updateResult.Message);
                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
