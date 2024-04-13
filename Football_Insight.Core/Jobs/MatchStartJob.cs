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
                if (context.JobDetail.JobDataMap.TryGetValue("matchId", out var matchObj) && int.TryParse(matchObj.ToString(), out int matchId) && matchId != 0)
                {
                    matchTimerService.UpdateMatchMinute(matchId);
                }
                else
                {
                    //log
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Task.CompletedTask;
        }
    }
}
