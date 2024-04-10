using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Goal;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Football_Insight.Core.Services
{
    public class GoalService : IGoalService
    {
        private readonly IRepository repository;
        private readonly IMatchTimerService matchTimerService;
        private readonly IMemoryCache memoryCache;
        private readonly ICacheService cacheService;

        public GoalService(IRepository _repository, IMatchTimerService _matchTimerService, IMemoryCache _memoryCache, ICacheService _cacheService)
        {
            repository = _repository;
            matchTimerService = _matchTimerService;
            memoryCache = _memoryCache;
            cacheService = _cacheService;
        }

        public async Task<OperationResult> AddGoalAsync(GoalModalViewModel viewModel)
        {
            var cacheKey = $"Match_{viewModel.MatchId}_Status";
            bool statusHasValue = cacheService.TryGetCachedItem(cacheKey);

            MatchStatus status = MatchStatus.Scheduled;
            if (statusHasValue)
            {
                status = memoryCache.Get<MatchStatus>(cacheKey);
            }

            if (status != MatchStatus.FirstHalf && status != MatchStatus.SecondHalf)
            {
                return new OperationResult(false, "You can only add a goal when the match is in the first half or the second half.");
            }

            if(viewModel.PlayerScorerId == 0)
            {
                return new OperationResult(false, "Goal scorer is required..");
            }

            if (viewModel.PlayerAssistedId == viewModel.PlayerScorerId)
            {
                return new OperationResult(false, "Goal scorer and assistant cannot be same person.");
            }

            var goal = new Goal();

            if (viewModel.PlayerAssistedId == 0)
            {
                goal = new Goal
                {
                    MatchId = viewModel.MatchId,
                    GoalScorerId = viewModel.PlayerScorerId,
                    GoalMinute = matchTimerService.GetMatchMinute(viewModel.MatchId),
                    TeamId = viewModel.TeamId
                };
            }
            else
            {
                goal = new Goal
                {
                    MatchId = viewModel.MatchId,
                    GoalScorerId = viewModel.PlayerScorerId,
                    GoalAssistantId = viewModel.PlayerAssistedId,
                    GoalMinute = matchTimerService.GetMatchMinute(viewModel.MatchId),
                    TeamId = viewModel.TeamId
                };
            }

            var match = await repository.GetByIdAsync<Match>(viewModel.MatchId);
            if(match.HomeTeamId == goal.TeamId)
            {
                match.HomeScore += 1;
            }
            else if(match.AwayTeamId == goal.TeamId)
            {
                match.AwayScore += 1;
            }

            await repository.AddAsync(goal);
            await repository.SaveChangesAsync();

            return new OperationResult(true, "Goal added succesffully.");
        }

        public async Task<List<GoalSimpleModelView>> GetGoalsAsync(int matchId)
        {
            var goals = await repository.AllReadonly<Goal>(g => g.MatchId == matchId)
                .Select(g => new GoalSimpleModelView
                {
                    ScorerName = $"{g.GoalScorer.FirstName} {g.GoalScorer.LastName}",
                    AssistantName = $"{g.GoalAssistant.FirstName} {g.GoalAssistant.LastName}".Trim(),
                    GoalTime = g.GoalMinute,
                    TeamId = g.TeamId
                })
                .ToListAsync();

            return goals;
        }
    }
}
