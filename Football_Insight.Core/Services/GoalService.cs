using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Goal;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Models;

namespace Football_Insight.Core.Services
{
    public class GoalService : IGoalService
    {
        private readonly IRepository repository;
        private readonly IMatchTimerService matchTimerService;

        public GoalService(IRepository _repository, IMatchTimerService _matchTimerService)
        {
            repository = _repository;
            matchTimerService = _matchTimerService;
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
            };

            await repository.AddAsync(goal);
            await repository.SaveChangesAsync();

            return new OperationResult(true, "Goal added succesffully");
        }
    }
}
