using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Goal;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Core.Services
{
    public class GoalService : IGoalService
    {
        private readonly IRepository repository;
        private readonly IMatchTimerService matchTimerService;
        private readonly IMatchService matchService;

        public GoalService(IRepository _repository, IMatchTimerService _matchTimerService, IMatchService _matchService)
        {
            repository = _repository;
            matchTimerService = _matchTimerService;
            matchService = _matchService;
        }

        public async Task<OperationResult> AddGoalAsync(GoalModalViewModel viewModel)
        {
            if(viewModel.PlayerAssistedId == viewModel.PlayerScorerId)
            {
                return new OperationResult(false, "Goal scorer and assistant cannot be same person.");
            }

            var goal = new Goal
            {
                MatchId = viewModel.MatchId,
                GoalScorerId = viewModel.PlayerScorerId,
                GoalAssistantId = viewModel.PlayerAssistedId,
                GoalMinute = matchTimerService.GetMatchMinute(viewModel.MatchId),
                TeamId = viewModel.TeamId
            };

            var match = await repository.GetByIdAsync<Match>(viewModel.MatchId);
            var goals = (await repository.AllReadonly<Goal>(g => g.MatchId == viewModel.MatchId && g.TeamId == viewModel.TeamId).ToListAsync()).Count;

            await repository.AddAsync(goal);
            await repository.SaveChangesAsync();

            return new OperationResult(true, "Goal added succesffully");
        }
    }
}
