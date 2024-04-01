using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Match;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Football_Insight.Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly IRepository repo;
        private readonly IStadiumService stadiumService;
        private readonly ILeagueService leagueService;
        private readonly ITeamService teamService;

        public MatchService(IRepository _repo, IStadiumService _stadiumService, ILeagueService _leagueService, ITeamService _teamService)
        {
            repo = _repo;
            teamService = _teamService;
            stadiumService = _stadiumService;
            leagueService = _leagueService;
        }

        public async Task<MatchDetailsViewModel> GetMatchDetailsAsync(int matchId)
        {
            var match = await GetMatchAsync(matchId);

            var viewModel = new MatchDetailsViewModel
            {
                Id = match.Id,
                HomeTeamName = await teamService.GetTeamNameAsync(match.HomeTeamId),
                HomeTeamId = match.HomeTeamId,
                HomeScore = match.HomeScore,
                AwayTeamName = await teamService.GetTeamNameAsync(match.AwayTeamId),
                AwayTeamId = match.AwayTeamId,
                AwayScore = match.AwayScore,
                DateAndTime = match.Date.ToString(),
                Status = match.Status.ToString(),
                LeagueId = match.LeagueId,
            };

            return viewModel;
        }

        public async Task<int> CreateMatchAsync(MatchFormViewModel model, int leagueId)
        {
            var stadiumId = await stadiumService.GetStadiumIdAsync(model.HomeTeamId);

            var match = new Match
            {
                Date = model.DateAndTime,
                HomeTeamId = model.HomeTeamId,
                AwayTeamId = model.AwayTeamId,
                StadiumId = stadiumId,
                HomeScore = 0,
                AwayScore = 0,
                LeagueId = leagueId,
                Status = MatchStatus.Scheduled
            };

            await repo.AddAsync(match);
            await repo.SaveChangesAsync();

            return match.Id;
        }

        public async Task UpdateMatchAsync(MatchFormViewModel model, int matchId)
        {
            var match = await GetMatchAsync(matchId);

            if (match != null)
            {
                match.HomeTeamId = model.HomeTeamId;
                match.AwayTeamId = model.AwayTeamId;
                match.Date = model.DateAndTime;

                await repo.SaveChangesAsync();
            }
        }

        public async Task<MatchFormViewModel?> GetMatchFormViewModelByIdAsync(int matchId)
        {
            var match = await GetMatchAsync(matchId);

            if (match != null)
            {
                var model = new MatchFormViewModel
                {
                    HomeTeamId = match.HomeTeamId,
                    AwayTeamId = match.AwayTeamId,
                    DateAndTime = match.Date,
                    LeagueId = match.LeagueId,
                    Teams = await leagueService.GetAllTeamsAsync(match.LeagueId)
                };

                return model;
            }

            return null;
        }

        public async Task<ActionResult> StartMatchAsync(int matchId)
        {
            var match = await GetMatchAsync(matchId);

            if(match.Date >= DateTime.Now && match.Status == MatchStatus.Scheduled) 
            {
                match.Status = MatchStatus.Live;
                await repo.SaveChangesAsync();

                return new ActionResult(true, "Match started successfully!");
            }

            return new ActionResult(false, "Cannot start a match that is scheduled for the future.");
        }

        public async Task<MatchSimpleViewModel> FindMatchAsync(int matchId)
        {
            var match = await GetMatchAsync(matchId);

            var model = new MatchSimpleViewModel
            {
                MatchId = match.Id,
                HomeTeam = await teamService.GetTeamNameAsync(match.HomeTeamId),
                AwayTeam = await teamService.GetTeamNameAsync(match.AwayTeamId),
                LeagueId = match.LeagueId
            };

            return model;
        }

        private async Task<Match> GetMatchAsync(int matchId) => await repo.GetByIdAsync<Match>(matchId);

        public async Task<ActionResult> DeleteMatchAsync(int matchId)
        {
            var match = await repo.GetByIdAsync<Match>(matchId);

            if (match == null)
            {
                return new ActionResult(false, "Match not found!");
            }

            await repo.RemoveAsync(match);
            await repo.SaveChangesAsync();

            return new ActionResult(true, $"Successfully deleted {match.Id}!");
        }
    }
}
