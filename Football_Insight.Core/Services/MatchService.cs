using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
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

        public MatchService(IRepository _repo, IStadiumService _stadiumService)
        {
            repo = _repo;
            stadiumService = _stadiumService;
        }

        public async Task<ActionResult> CreateMatchAsync(MatchCreateViewModel model)
        {
            if (model.HomeTeamId == model.AwayTeamId)
            {
                return new ActionResult(false, "The home team and away team cannot be the same.");
            }

            var stadiumId = await stadiumService.GetStadiumIdAsync(model.HomeTeamId);

            var match = new Match
            {
                Date = model.DateTime,
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
    }
}
