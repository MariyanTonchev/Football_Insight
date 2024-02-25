using Football_Insight.Data;

public static class DataSeeder
{
    public static void InitializeAndSeedDatabase(IServiceProvider serviceProvider)
    {
        using (var serviceScope = serviceProvider.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<FootballInsightDbContext>();
            // Check for data and seed if necessary
            // ...
        }
    }
}