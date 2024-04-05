namespace Football_Insight.Core.Contracts
{
    public interface IMatchJobService
    {
        Task StartMatchJobAsync(int matchId);

        Task PauseMatchJobAsync(int matchId);

        Task UnpauseMatchJobAsync(int matchId);
    }
}
