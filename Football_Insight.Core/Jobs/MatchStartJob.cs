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
            try
            {
                int matchId = context.JobDetail.JobDataMap.GetInt("matchId");
                if (matchId != 0)
                {
                    matchTimerService.UpdateMatchMinute(matchId);
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return Task.CompletedTask;
            }
        }
    }
}
