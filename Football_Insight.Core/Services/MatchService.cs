using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Match;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;

namespace Football_Insight.Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly IRepository repo;
        private readonly IStadiumService stadiumService;
        private readonly ILeagueService leagueService;

        public MatchService(IRepository _repo, IStadiumService _stadiumService, ILeagueService _leagueService)
        {
            repo = _repo;
            stadiumService = _stadiumService;
            leagueService = _leagueService;
        }

        public async Task<ActionResult> CreateMatchAsync(MatchCreateViewModel model)
        {
            if (model.HomeTeamId == 0 && model.AwayTeamId == 0)
            {
                return new ActionResult(false, "Home Team and Away Team are required! Please select!");
            }

            if (model.HomeTeamId == 0)
            {
                return new ActionResult(false, "Home Team is required! Please select!");
            }

            if (model.AwayTeamId == 0)
            {
                return new ActionResult(false, "Away Team is required! Please select!");
            }


            if (model.HomeTeamId == model.AwayTeamId)
            {
                return new ActionResult(false, "The home team and away team cannot be the same.");
            }

            var stadiumId = await stadiumService.GetStadiumIdAsync(model.HomeTeamId);

            var match = new Match
            {
                Date = model.DateAndTime,
                HomeTeamId = model.HomeTeamId,
                AwayTeamId = model.AwayTeamId,
                StadiumId = stadiumId,
                HomeScore = 0,
                AwayScore = 0,
                LeagueId = model.LeagueId,
                Status = MatchStatus.Scheduled
            };

            await repo.AddAsync(match);
            await repo.SaveChangesAsync();

            return new ActionResult(true, "Match created successfully.");
        }

        public async Task<MatchEditViewModel> GetMatchDetailsAsync(int matchId)
        {
            var match = await repo.GetByIdAsync<Match>(matchId);

            var viewModel = new MatchEditViewModel
            {
                Id = match.Id,
                HomeTeamId = match.HomeTeamId,
                AwayTeamId = match.AwayTeamId,
                DateAndTime = match.Date,
                Teams = await leagueService.GetAllTeamsAsync(match.LeagueId) 
            };

            return viewModel;
        }

        public async Task<ActionResult> UpdateMatchAsync(MatchEditViewModel model)
        {
            var match = await repo.GetByIdAsync<Match>(model.Id);

            if (match == null)
            {
                return new ActionResult(false, "Match not found!");
            }

            match.HomeTeamId = model.HomeTeamId;
            match.AwayTeamId = model.AwayTeamId;
            match.Date = model.DateAndTime;

            try
            {
                await repo.SaveChangesAsync();
                return new ActionResult(true, "Match edited successfully.");
            }
            catch (Exception ex)
            {
                return new ActionResult(false, ex.Message);
            }
        }
    }
}
