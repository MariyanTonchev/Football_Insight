using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Match;
using Football_Insight.Core.Models.Team;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Core.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly IRepository repo;

        public LeagueService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<ActionResult> CreateLeagueAsync(LeagueCreateViewModel model)
        {
            var existingLeagues = await GetAllLeaguesAsync();

            if (existingLeagues.Any(l => l.Name.Equals(model.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return new ActionResult(false, "A league with the same name already exists.");
            }

            var newLeague = new League
            {
                Name = model.Name,
            };

            await repo.AddAsync(newLeague);
            await repo.SaveChangesAsync();

            return new ActionResult(true, "League created successfully.", newLeague.Id);
        }

        public async Task<ActionResult> DeleteLeagueAsync(int leagueId)
        {
            var league = await repo.GetByIdAsync<League>(leagueId);

            if (league == null)
            {
                return new ActionResult(false, "League not found!");
            }

            var teamsInLeagueCount = (await GetAllTeamsAsync(leagueId)).Count;

            if(teamsInLeagueCount > 0)
            {
                return new ActionResult(false, $"League has {teamsInLeagueCount} teams and cannot be deleted!");
            }

            await repo.RemoveAsync(league);
            await repo.SaveChangesAsync();

            return new ActionResult(true, $"Successfully deleted {league.Name}!");
        }

        public async Task<LeagueSimpleViewModel> FindLeagueAsync(int leagueId)
        {
            var league = await repo.GetByIdAsync<League>(leagueId);

            var viewModel = new LeagueSimpleViewModel
            {
                Id = league.Id,
                Name = league.Name,
            };

            return viewModel;
        }

        public async Task<List<LeagueSimpleViewModel>> GetAllLeaguesAsync()
        {
            var leagues = await repo.AllReadonly<League>()
                .Select(l => new LeagueSimpleViewModel 
                {
                    Id = l.Id,
                    Name = l.Name
                })
                .ToListAsync();

            return leagues;
        }

        public async Task<List<LeagueTeamsViewModel>> GetAllLeaguesWithTeamsAsync()
        {
            var leagues = await repo.All<League>()
                        .Select(l => new LeagueTeamsViewModel
                        {
                            Id = l.Id,
                            Name = l.Name,
                            Teams = repo.All<Team>()
                                .Where(t => t.LeagueId == l.Id)
                                .Select(t => new TeamSimpleViewModel
                                {
                                    Id = t.Id,
                                    Name = t.Name,
                                })
                                .ToList()
                        })
                        .ToListAsync();

            return leagues;
        }

        public async Task<List<TeamSimpleViewModel>> GetAllTeamsAsync(int leagueId)
        {
            return await repo.AllReadonly<Team>(t => t.LeagueId == leagueId)
                .Select(t => new TeamSimpleViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }

        public async Task<LeagueEditViewModel> GetLeagueDetailsAsync(int leagueId)
        {
            var league = await repo.GetByIdAsync<League>(leagueId);

            var viewModel = new LeagueEditViewModel
            {
                Id = league.Id,
                Name = league.Name
            };

            return viewModel;
        }

        public async Task<LeagueMatchesViewModel> GetLeagueViewDataAsync(int leagueId)
        {
            var leagueName = (await repo.GetByIdAsync<League>(leagueId)).Name;

            var viewModel = new LeagueMatchesViewModel()
            {
                Id = leagueId,
                LeagueName = leagueName,
                Matches = await GetRecentMatchesAsync(leagueId),
                Teams = await GetTeamTableAsync(leagueId)
            };

            return viewModel;
        }

        public async Task<List<MatchLeagueViewModel>> GetRecentMatchesAsync(int leagueId)
        {
            var matches = await repo.AllReadonly<Match>(t => t.LeagueId == leagueId)
                    .OrderByDescending(m => m.Date)
                    .Select(m => new MatchLeagueViewModel()
                    {
                        Id = m.Id,
                        HomeTeamName = m.HomeTeam.Name,
                        AwayTeamName = m.AwayTeam.Name,
                        DateAndTime = m.Date.ToString("HH:mm dd/MM/yyyy")
                    })
                    .ToListAsync();

            return matches;
        }

        public async Task<List<TeamTableViewModel>> GetTeamTableAsync(int leagueId)
        {
            var teams = await repo.AllReadonly<Team>(t => t.LeagueId == leagueId)
                    .Select(t => new TeamTableViewModel()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Wins =
                            t.HomeMatches.Where(hm => hm.HomeScore > hm.AwayScore && hm.Status == MatchStatus.Finished).Count() +
                            t.AwayMatches.Where(am => am.AwayScore > am.HomeScore && am.Status == MatchStatus.Finished).Count(),
                        Draws =
                            t.HomeMatches.Where(hm => hm.HomeScore == hm.AwayScore && hm.Status == MatchStatus.Finished).Count() +
                            t.AwayMatches.Where(am => am.AwayScore == am.HomeScore && am.Status == MatchStatus.Finished).Count(),
                        Losses =
                            t.HomeMatches.Where(hm => hm.HomeScore < hm.AwayScore && hm.Status == MatchStatus.Finished).Count() +
                            t.AwayMatches.Where(am => am.AwayScore < am.HomeScore && am.Status == MatchStatus.Finished).Count(),
                        GoalsAgainst =
                            t.AwayMatches.Sum(am => am.HomeScore) +
                            t.HomeMatches.Sum(hm => hm.AwayScore),
                        GoalsFor =
                            t.AwayMatches.Sum(am => am.AwayScore) +
                            t.HomeMatches.Sum(hm => hm.HomeScore),
                        Logo = t.LogoURL,
                        Points =
                            t.HomeMatches.Where(hm => hm.HomeScore > hm.AwayScore && hm.Status == MatchStatus.Finished).Count() * 3 +
                            t.AwayMatches.Where(am => am.AwayScore > am.HomeScore && am.Status == MatchStatus.Finished).Count() * 3 +
                            t.HomeMatches.Where(hm => hm.HomeScore == hm.AwayScore && hm.Status == MatchStatus.Finished).Count() +
                            t.AwayMatches.Where(am => am.AwayScore == am.HomeScore && am.Status == MatchStatus.Finished).Count(),
                    })
                    .OrderBy(t => t.Points)
                    .ToListAsync();

            return teams;
        }

        public async Task<ActionResult> UpdateLeagueAsync(LeagueEditViewModel viewModel)
        {
            var league = await repo.GetByIdAsync<League>(viewModel.Id);

            if (league == null)
            {
                return new ActionResult(false, "League not found!");
            }

            var existingLeagues = await GetAllLeaguesAsync();

            if (existingLeagues.Any(l => l.Name.Equals(viewModel.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return new ActionResult(false, "A league with the same name already exists.");
            }

            league.Name = viewModel.Name;

            try
            {
                await repo.SaveChangesAsync();
                return new ActionResult(true, "League edited successfully.");
            }
            catch (Exception ex)
            {
                return new ActionResult(false, ex.Message);
            }
        }
    }
}
