using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Player;
using Football_Insight.Core.Models.Team;

namespace Football_Insight.Core.Contracts
{
    public interface ITeamService
    {
        Task<TeamFormViewModel> GetCreateFormViewModel();

        Task<OperationResult> CreateTeamAsync(TeamFormViewModel viewModel);

        Task<TeamFormViewModel> GetEditFormViewModel(int teamId);

        Task<OperationResult> UpdateTeamAsync(TeamFormViewModel viewModel, int teamId);

        Task<OperationResult> DeleteTeamAsync(int teamId);

        Task<TeamFixturesViewModel> GetTeamFixturesAsync(int id);

        Task<TeamResultsViewModel> GetTeamResultsAsync(int id);

        Task<TeamSquadViewModel> GetTeamSquadAsync(int id);

        Task<List<TeamSimpleViewModel>> GetAllTeamsAsync();

        Task<TeamSimpleViewModel> GetTeamSimpleViewModelAsync(int teamId);

        Task<List<PlayerSimpleViewModel>> GetSquadAsync(int teamId);

        Task<string> GetTeamNameAsync(int id);
    }
}
