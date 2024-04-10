using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Player;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Controllers
{
    public class PlayerController : BaseController
    {
        private readonly IPlayerService playerService;

        public PlayerController(IPlayerService _playerService)
        {
            playerService = _playerService;
        }

        [HttpGet]
        public IActionResult Create(int teamId)
        {
            var viewModel = playerService.GetCreateFomViewModel(teamId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerFormViewModel viewModel)
        {
            var result = await playerService.CreatePlayerAsync(viewModel);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction("Squad", "Team", new {Id = viewModel.TeamId});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int playerId)
        {
            var viewModel = await playerService.GetEditFormViewModel(playerId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlayerFormViewModel viewModel, int playerId)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            var result = await playerService.UpdatePlayerAsync(viewModel, playerId);

            if (!result.Success)
            {
                viewModel.Positions = playerService.GetPositionsFromEnum();
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction("Squad", "Team", new { Id = viewModel.TeamId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int playerId)
        {
            var viewModel = await playerService.GetPlayerSimpleViewModelAsync(playerId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PlayerSimpleViewModel viewModel)
        {
            var result = await playerService.DeletePlayerAsync(viewModel.PlayerId);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(viewModel);
            }

            return RedirectToAction("Squad", "Team", new { Id = viewModel.TeamId });
        }
    }
}
