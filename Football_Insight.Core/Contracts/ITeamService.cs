using Football_Insight.Core.Models.Player;
using Football_Insight.Core.Models.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Insight.Core.Contracts
{
    public interface ITeamService
    {
        Task<TeamFixturesViewModel> GetTeamFixturesAsync(int id);

        Task<TeamResultsViewModel> GetTeamResultsAsync(int id);

        Task<TeamSquadViewModel> GetTeamSquadAsync(int id);

        Task<List<TeamSimpleViewModel>> GetAllTeamsAsync();

        Task<List<PlayerSimpleViewModel>> GetSquadAsync(int teamId);

        Task<string> GetTeamNameAsync(int id);
    }
}
