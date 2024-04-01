using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Match;
using Football_Insight.Core.Models.Player;
using Football_Insight.Core.Models.Team;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Football_Insight.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRepository repo;

        public TeamService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<List<TeamSimpleViewModel>> GetAllTeamsAsync()
        {
            var teams = await repo.AllReadonly<Team>()
                                  .Select(t => new TeamSimpleViewModel
                                  {
                                      Id = t.Id,
                                      Name = t.Name,
                                  })
                                  .ToListAsync();

            return teams;
        }

        public async Task<TeamFixturesViewModel> GetTeamFixturesAsync(int id)
        {
            var matches = await repo.All<Match>()
                .Include(m => m.HomeTeam)
                .Include(t => t.AwayTeam)
                .Where(m => (m.HomeTeamId == id || m.AwayTeamId == id) && (m.Status == MatchStatus.Scheduled))
                .Select(m => new MatchFixtureViewModel
                {
                    Id = m.Id,
                    HomeTeam = m.HomeTeam.Name,
                    AwayTeam = m.AwayTeam.Name,
                    Date = m.Date.ToString(Constants.MessageConstants.DateFormat)
                })
                .ToListAsync();

            var teamFixtures = new TeamFixturesViewModel
            {
                TeamId = id,
                Fixtures = matches
            };

            return teamFixtures;
        }

        public async Task<string> GetTeamNameAsync(int id)
        {
            var team = await repo.GetByIdAsync<Team>(id);

            return team.Name;
        }

        public async Task<TeamResultsViewModel> GetTeamResultsAsync(int id)
        {
            var matches = await repo.All<Match>()
                .Include(t => t.HomeTeam)
                .Include(t => t.AwayTeam)
                .Where(m => (m.HomeTeamId == id || m.AwayTeamId == id) && (m.Status == MatchStatus.Finished))
                .Select(m => new MatchResultViewModel
                {
                    Id = m.Id,
                    HomeTeam = m.HomeTeam.Name,
                    AwayTeam = m.AwayTeam.Name,
                    HomeTeamGoals = m.HomeScore,
                    AwayTeamGoals = m.AwayScore,
                    Date = m.Date.ToString()
                })
                .ToListAsync();

            var teamFixtures = new TeamResultsViewModel
            {
                TeamId = id,
                Results = matches
            };

            return teamFixtures;
        }

        public async Task<TeamSquadViewModel> GetTeamSquadAsync(int id)
        {
            var players = await repo.All<Player>()
                .Where(p => p.TeamId == id)
                .Select(p => new PlayerSquadViewModel
                {

                })
                .ToListAsync();

            var teamSquad = new TeamSquadViewModel
            {
                TeamId = id,
                Players = players
            };

            return teamSquad;
        }
    }
}
