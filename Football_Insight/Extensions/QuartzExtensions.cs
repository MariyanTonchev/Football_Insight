using Quartz;

namespace Football_Insight.Extensions
{
    public static class QuartzExtensions
    {
        public static void AddJobAndTrigger<T>(this IServiceCollectionQuartzConfigurator quartz) where T : IJob
        {
            var jobKey = new JobKey(typeof(T).Name);
            quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));
            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity($"{typeof(T).Name}-trigger") 
            );
        }
    }
}