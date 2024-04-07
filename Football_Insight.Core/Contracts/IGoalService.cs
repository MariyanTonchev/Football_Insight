using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Goal;

namespace Football_Insight.Core.Contracts
{
    public interface IGoalService
    {
        Task<OperationResult> AddGoalAsync(GoalModalViewModel viewModel);

        Task<List<GoalSimpleModelView>> GetGoalsAsync(int matchId);
    }
}
