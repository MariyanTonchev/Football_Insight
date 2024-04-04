using Football_Insight.Core.Models;
using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Match;
using Football_Insight.Core.Models.Team;

namespace Football_Insight.Core.Contracts
{
    public interface ILeagueService
    {
        Task<List<LeagueTeamsViewModel>> GetAllLeaguesWithTeamsAsync();
        Task<List<TeamSimpleViewModel>> GetAllTeamsAsync(int leagueId);
        Task<List<TeamTableViewModel>> GetTeamTableAsync(int leagueId);
        Task<List<MatchLeagueViewModel>> GetRecentMatchesAsync(int leagueId);
        Task<LeagueMatchesViewModel> GetLeagueViewDataAsync(int leagueId);
        Task<List<LeagueSimpleViewModel>> GetAllLeaguesAsync();
        Task<OperationResult> CreateLeagueAsync(LeagueCreateViewModel model);
        Task<LeagueEditViewModel> GetLeagueDetailsAsync(int leagueId);
        Task<LeagueSimpleViewModel> FindLeagueAsync(int leagueId);
        Task<OperationResult> UpdateLeagueAsync(LeagueEditViewModel viewModel);
        Task<OperationResult> DeleteLeagueAsync(int leagueId);
    }
}
