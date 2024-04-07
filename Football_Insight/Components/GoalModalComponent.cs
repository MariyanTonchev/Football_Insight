using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Goal;
using Microsoft.AspNetCore.Mvc;

namespace Football_Insight.Components
{
    public class GoalModalComponent : ViewComponent
    {
        private readonly ITeamService teamService;

        public GoalModalComponent(ITeamService _teamService)
        {
            teamService = _teamService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int teamId, int matchId)
        {
            var players = await teamService.GetSquadAsync(teamId);

            var viewModel = new GoalModalViewModel
            {
                MatchId = matchId,
                TeamId = teamId,
                Players = players,
            };

            return await Task.FromResult<IViewComponentResult>(View(viewModel));
        }
    }
}
