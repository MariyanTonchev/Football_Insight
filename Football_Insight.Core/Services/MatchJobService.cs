using Football_Insight.Core.Contracts;
using Football_Insight.Jobs;
using Quartz;

namespace Football_Insight.Core.Services
{
    public class MatchJobService : IMatchJobService
    {
        private readonly ISchedulerFactory schedulerFactory;

        public MatchJobService(ISchedulerFactory _schedulerFactory)
        {
            schedulerFactory = _schedulerFactory;
        }

        public async Task StartMatchJobAsync(int matchId)
        {
            var scheduler = await schedulerFactory.GetScheduler();
            var jobKey = new JobKey($"StartMatchJob-{matchId}");
            var triggerKey = new TriggerKey($"MatchStartTrigger-{matchId}");


            var job = JobBuilder.Create<MatchStartJob>()
                        .WithIdentity(jobKey)
                        .UsingJobData("matchId", matchId)
                        .Build();

            var trigger = TriggerBuilder.Create()
                        .WithIdentity(triggerKey)
                        .StartNow()
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(Constants.MessageConstants.SettingSecondsInOneMinute).RepeatForever())
                        .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public async Task PauseMatchJobAsync(int matchId)
        {
            var scheduler = await schedulerFactory.GetScheduler();
            var jobKey = new JobKey($"StartMatchJob-{matchId}");

            await scheduler.PauseJob(jobKey);
        }

        public async Task UnpauseMatchJobAsync(int matchId)
        {
            var scheduler = await schedulerFactory.GetScheduler();
            var jobKey = new JobKey($"StartMatchJob-{matchId}");

            await scheduler.ResumeJob(jobKey);
        }

        public async Task EndMatchJobAsync(int matchId)
        {
            var scheduler = await schedulerFactory.GetScheduler();
            var jobKey = new JobKey($"StartMatchJob-{matchId}");

            await scheduler.DeleteJob(jobKey);
        }
    }
}
