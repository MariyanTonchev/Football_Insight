using Football_Insight.Core.Contracts;
using Quartz;

namespace Football_Insight.Jobs
{
    public class MatchStartJob : IJob
    {
        private readonly IMatchTimerService matchTimerService;

        public MatchStartJob(IMatchTimerService _matchTimerService)
        {
            matchTimerService = _matchTimerService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            int matchId = context.JobDetail.JobDataMap.GetInt("matchId");

            if (matchId != 0)
            {
                matchTimerService.UpdateMatchMinute(matchId);
            }

            return Task.CompletedTask;
        }
    }
}
