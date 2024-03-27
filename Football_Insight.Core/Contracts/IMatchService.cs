using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Match;

namespace Football_Insight.Core.Contracts
{
    public interface IMatchService
    {
        Task<int> CreateMatchAsync(MatchFormViewModel model, int leagueId);

        Task UpdateMatchAsync(MatchFormViewModel model, int matchId);

        Task<MatchDetailsViewModel> GetMatchDetailsAsync(int matchId);

        Task<MatchFormViewModel?> GetMatchFormViewModelByIdAsync(int id);
    }
}
