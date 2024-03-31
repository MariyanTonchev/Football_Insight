using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Match;
using Football_Insight.Infrastructure.Data.Models;
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
            var viewModel = new MatchFormViewModel()
            {
                Teams = await leagueService.GetAllTeamsAsync(leagueId)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MatchFormViewModel model, int leagueId)
        {
            if (!ModelState.IsValid)
            {
                model.Teams = await leagueService.GetAllTeamsAsync(leagueId);
                return View(model);
            }

            await matchService.CreateMatchAsync(model, leagueId);

            return RedirectToAction("Index", "League", new { id = leagueId});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int matchId)
        {
            var match = await matchService.GetMatchFormViewModelByIdAsync(matchId);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MatchFormViewModel viewModel, int matchId)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                viewModel.Teams = await leagueService.GetAllTeamsAsync(viewModel.LeagueId);

                return View(viewModel);
            }

            await matchService.UpdateMatchAsync(viewModel, matchId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Start(int matchId)
        {
            var viewModel = await matchService.FindMatchAsync(matchId);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Start(MatchSimpleViewModel viewModel)
        {
            await matchService.StartMatchAsync(viewModel.Id);

            return RedirectToAction(nameof(Index), new { Id = viewModel.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int matchId)
        {
            var match = await matchService.FindMatchAsync(matchId);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        [HttpPost]
       
        public async Task<IActionResult> Delete(MatchSimpleViewModel model)
        {
            var result = await matchService.DeleteMatchAsync(model.Id);

            if (result.Success == false)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction("Index", "League", new { id = model.LeagueId });
        }
    }
}
