using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Models;
using Quartz;

namespace Football_Insight.Jobs
{
    public class MatchStartJob : IJob
    {
        private readonly IRepository repository;

        public MatchStartJob(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            int matchId = context.JobDetail.JobDataMap.GetInt("matchId");

            var match = await repository.GetByIdAsync<Match>(matchId);

            if (match != null)
            {
                Console.WriteLine($"Executing MatchStartJob at {DateTime.Now}");
                match.Minutes += 1;
                await repository.SaveChangesAsync();
            }
        }
    }
}
