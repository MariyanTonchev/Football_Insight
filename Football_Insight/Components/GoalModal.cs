using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Match;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Components
{
    public class GoalModal : ViewComponent
    {
        private readonly ITeamService teamService;

        public GoalModal(ITeamService _teamService)
        {
            teamService = _teamService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int teamId)
        {
            var players = await teamService.GetSquadAsync(teamId);

            var viewModel = new GoalModalViewModel
            {
                Players = players,
            };

            return await Task.FromResult<IViewComponentResult>(View(viewModel));
        }
    }
}
