namespace Football_Insight.Core.Contracts
{
    public interface IMatchTimerService
    {
        void UpdateMatchMinute(int matchId);

        int GetMatchMinute(int matchId);

        Task PersistMinutesToDatabase(int matchId);
    }
}
