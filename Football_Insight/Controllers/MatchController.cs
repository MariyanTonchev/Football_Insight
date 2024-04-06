using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Match;
using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace Football_Insight.Controllers
{
    public class MatchController : BaseController
    {
        private readonly IMatchService matchService;
        private readonly ILeagueService leagueService;
        private readonly ISchedulerFactory schedulerFactory;
        private readonly IMatchTimerService matchTimerService;
        private readonly ILogger<MatchController> logger;

        public MatchController(IMatchService _matchService, 
                        ILeagueService _leagueService, 
                        ISchedulerFactory _schedulerFactory, 
                        ILogger<MatchController> _logger, 
                        IMatchTimerService _matchTimerService)
        {
            matchService = _matchService;
            leagueService = _leagueService;
            schedulerFactory = _schedulerFactory;
            logger = _logger;
            matchTimerService = _matchTimerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int matchId)
        {
            var viewModel = await matchService.GetMatchDetailsAsync(matchId);

            if (viewModel == null)
            {
                return BadRequest();
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int leagueId)
        {
            var viewModel = new MatchFormViewModel()
            {
                LeagueId = leagueId,
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

            var result = await matchService.UpdateMatchAsync(viewModel, matchId);

            if (!result.Success)
            {
                viewModel.Teams = await leagueService.GetAllTeamsAsync(viewModel.LeagueId);
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index), new {matchId});
        }

        [HttpGet]
        public async Task<IActionResult> Start(int matchId)
        {
            var viewModel = await matchService.GetMatchSimpleViewAsync(matchId);

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
            var result = await matchService.StartMatchAsync(viewModel.MatchId);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index), new { viewModel.MatchId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int matchId)
        {
            var match = await matchService.GetMatchSimpleViewAsync(matchId);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(MatchSimpleViewModel model)
        {
            var result = await matchService.DeleteMatchAsync(model.MatchId);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction("Index", "League", new { id = model.LeagueId });
        }

        [HttpGet]
        public IActionResult GetMatchMinutes(int matchId)
        {
            var data = new
            {
                Minutes = matchTimerService.GetMatchMinute(matchId)
            };

            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> Pause(int matchId)
        {
            var match = await matchService.GetMatchSimpleViewAsync(matchId);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pause(MatchSimpleViewModel viewModel)
        {
            var result = await matchService.PauseMatchAsync(viewModel.MatchId);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index), new { viewModel.MatchId });
        }

        [HttpGet]
        public async Task<IActionResult> Unpause(int matchId)
        {
            var match = await matchService.GetMatchSimpleViewAsync(matchId);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unpause(MatchSimpleViewModel viewModel)
        {
            var result = await matchService.UnpauseMatchAsync(viewModel.MatchId);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index), new { viewModel.MatchId });
        }

        [HttpGet]
        public async Task<IActionResult> End(int matchId)
        {
            var match = await matchService.GetMatchEndViewAsync(matchId);

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> End(MatchSimpleViewModel viewModel)
        {
            var result = await matchService.EndMatchAsync(viewModel.MatchId);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index), new { viewModel.MatchId });
        }
    }
}
