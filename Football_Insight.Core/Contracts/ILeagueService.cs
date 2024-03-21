using Football_Insight.Core.Models.League;

namespace Football_Insight.Core.Contracts
{
    public interface ILeagueService
    {
        Task<List<LeagueTeamsViewModel>> GetAllLeaguesWithTeamsAsync();
    }
}
