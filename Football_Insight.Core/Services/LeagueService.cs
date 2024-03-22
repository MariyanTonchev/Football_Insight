using Football_Insight.Core.Contracts;
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

        public async Task<LeagueMatchesViewModel> GetLeagueViewDataAsync(int leagueId)
        {
            var viewModel = new LeagueMatchesViewModel()
            {
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
    }
}
