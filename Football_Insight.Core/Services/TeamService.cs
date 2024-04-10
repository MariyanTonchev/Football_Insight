using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Match;
using Football_Insight.Core.Models.Player;
using Football_Insight.Core.Models.Team;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRepository repo;
        private readonly IStadiumService stadiumService;
        private readonly ILeagueService leagueService;

        public TeamService(IRepository _repo, IStadiumService _stadiumServcie, ILeagueService _leageuService)
        {
            repo = _repo;
            stadiumService = _stadiumServcie;
            leagueService = _leageuService;
        }

        public async Task<OperationResult> CreateTeamAsync(TeamFormViewModel viewModel)
        {
            var team = new Team
            {
                Name = viewModel.Name,
                Founded = viewModel.Founded,
                LogoURL = viewModel.LogoURL,
                Coach = viewModel.Coach,
                StadiumId = viewModel.StadiumId,
                LeagueId = viewModel.LeagueId,
            };

            await repo.AddAsync(team);
            await repo.SaveChangesAsync();

            return new OperationResult(true, "Successfully created team.", team.Id);
        }

        public async Task<List<TeamSimpleViewModel>> GetAllTeamsAsync()
        {
            var teams = await repo.AllReadonly<Team>()
                                  .Select(t => new TeamSimpleViewModel
                                  {
                                      TeamId = t.Id,
                                      Name = t.Name,
                                  })
                                  .ToListAsync();

            return teams;
        }

        public async Task<TeamSimpleViewModel> GetTeamSimpleViewModelAsync(int teamId)
        {
            var team = await GetTeamAsync(teamId);

            var viewModel = new TeamSimpleViewModel
            {
                TeamId = team.Id,
                Name = team.Name,
            };

            return viewModel;
        }

        public async Task<TeamFormViewModel> GetCreateFormViewModel()
        {
            var viewModel = new TeamFormViewModel
            {
                Leagues = await leagueService.GetAllLeaguesAsync(),
                Stadiums = await stadiumService.GetAllStadiumAsync()
            };

            return viewModel;
        }

        public async Task<TeamFormViewModel> GetEditFormViewModel(int teamId)
        {
            var team = await repo.GetByIdAsync<Team>(teamId);

            var viewModel = new TeamFormViewModel
            {
                Id = team.Id,
                Name = team.Name,
                Founded = team.Founded,
                LogoURL = team.LogoURL,
                Coach = team.Coach,
                LeagueId = team.LeagueId,
                StadiumId = team.StadiumId,
                Leagues = await leagueService.GetAllLeaguesAsync(),
                Stadiums = await stadiumService.GetAllStadiumAsync()
            };

            return viewModel;
        }

        public async Task<OperationResult> DeleteTeamAsync(int teamId)
        {
            var team = await GetTeamAsync(teamId);

            if (team == null)
            {
                return new OperationResult(false, "Team not found.");
            }

            if (await HasMatches(teamId))
            {
                return new OperationResult(false, "You cannot delete a team with matches.");
            }

            if (await HasPlayers(teamId))
            {
                return new OperationResult(false, "You cannot delete a team with players.");
            }

            await repo.RemoveAsync(team);
            await repo.SaveChangesAsync();

            return new OperationResult(true, $"Successfully deleted {team.Id}!");
        }

        public async Task<List<PlayerSimpleViewModel>> GetSquadAsync(int teamId)
        {
            var players = await repo.AllReadonly<Player>(p => p.TeamId == teamId)
                        .Select(p => new PlayerSimpleViewModel
                        {
                            PlayerId = p.Id,
                            Name = $"{p.FirstName} {p.LastName}"
                        })
                        .ToListAsync();

            return players;
        }

        public async Task<TeamFixturesViewModel> GetTeamFixturesAsync(int teamId)
        {
            var matches = await repo.All<Match>()
                .Include(m => m.HomeTeam)
                .Include(t => t.AwayTeam)
                .Where(m => (m.HomeTeamId == teamId || m.AwayTeamId == teamId) && (m.Status == MatchStatus.Scheduled))
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
                TeamId = teamId,
                Fixtures = matches
            };

            return teamFixtures;
        }

        public async Task<string> GetTeamNameAsync(int id)
        {
            var team = await repo.GetByIdAsync<Team>(id);

            return team.Name;
        }

        public async Task<TeamResultsViewModel> GetTeamResultsAsync(int teamId)
        {
            var matches = await repo.All<Match>()
                .Include(t => t.HomeTeam)
                .Include(t => t.AwayTeam)
                .Where(m => (m.HomeTeamId == teamId || m.AwayTeamId == teamId) && (m.Status == MatchStatus.Finished))
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
                TeamId = teamId,
                Results = matches
            };

            return teamFixtures;
        }

        public async Task<TeamSquadViewModel> GetTeamSquadAsync(int teamId)
        {
            var players = await repo.All<Player>()
                .Where(p => p.TeamId == teamId)
                .Select(p => new PlayerSquadViewModel
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    DateOfBirth = p.DateOfBirth,
                    Position = (PlayerPosition)p.Position,
                    Price = p.Price,
                    Salary = p.Salary,
                    GoalAssited = p.GoalAssisted.Count,
                    GoalScored = p.GoalsScored.Count,
                })
                .ToListAsync();

            var teamSquad = new TeamSquadViewModel
            {
                TeamId = teamId,
                Players = players
            };

            return teamSquad;
        }

        public async Task<OperationResult> UpdateTeamAsync(TeamFormViewModel viewModel, int teamId)
        {
            var team = await GetTeamAsync(teamId);

            if (team != null)
            {
                team.Name = viewModel.Name;
                team.LogoURL = viewModel.LogoURL;
                team.StadiumId = viewModel.StadiumId;
                team.LeagueId = viewModel.LeagueId;
                team.Coach = viewModel.Coach;
                team.Founded = viewModel.Founded;

                await repo.SaveChangesAsync();

                return new OperationResult(true, "Team edited successfully!");
            }
            else
            {
                return new OperationResult(false, "Team not found!");
            }
        }

        private async Task<Team> GetTeamAsync(int teamId) => await repo.GetByIdAsync<Team>(teamId);

        private async Task<bool> HasMatches(int teamId)
        {
            var matches = await repo.AllReadonly<Match>().Where(t => t.AwayTeamId == teamId || t.HomeTeamId == teamId).ToListAsync();

            return matches.Any();
        }

        private async Task<bool> HasPlayers(int teamId)
        {
            var players = await repo.AllReadonly<Player>().Where(p => p.TeamId == teamId).ToListAsync();

            return players.Any();
        }
    }
}
