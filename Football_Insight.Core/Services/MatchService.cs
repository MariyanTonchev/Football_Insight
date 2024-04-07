using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Match;
using Football_Insight.Infrastructure.Data.Common;
using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace Football_Insight.Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly IRepository repo;
        private readonly IStadiumService stadiumService;
        private readonly ILeagueService leagueService;
        private readonly ITeamService teamService;
        private readonly IMatchTimerService matchTimerService;
        private readonly IMatchJobService matchJobService;
        private readonly IMemoryCache memoryCache;
        private readonly IHttpContextAccessor httpContextAccessor;

        public MatchService(IRepository _repo, 
                            IStadiumService _stadiumService, 
                            ILeagueService _leagueService, 
                            ITeamService _teamService, 
                            IMatchTimerService _matchTimerService,
                            IMatchJobService _matchJobService,
                            IMemoryCache _memoryCache, 
                            IHttpContextAccessor _httpContextAccessor)
        {
            repo = _repo;
            teamService = _teamService;
            stadiumService = _stadiumService;
            leagueService = _leagueService;
            matchTimerService = _matchTimerService;
            matchJobService = _matchJobService;
            memoryCache = _memoryCache;
            httpContextAccessor = _httpContextAccessor;
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
                Status = match.Status,
                LeagueId = match.LeagueId,
                Minutes = matchTimerService.GetMatchMinute(matchId)
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

        public async Task<OperationResult> UpdateMatchAsync(MatchFormViewModel model, int matchId)
        {
            var match = await GetMatchAsync(matchId);

            if (match.Status != MatchStatus.Scheduled)
            {
                return new OperationResult(false, "You cannot edit a match that has started, finished, or been postponed.");
            }

            if (match != null)
            {
                match.HomeTeamId = model.HomeTeamId;
                match.AwayTeamId = model.AwayTeamId;
                match.Date = model.DateAndTime;

                await repo.SaveChangesAsync();
            }

            return new OperationResult(true, "Match edited successfully!");
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

        public async Task<OperationResult> StartMatchAsync(int matchId)
        {
            try
            {
                var match = await GetMatchAsync(matchId);

                if (match == null)
                {
                    return new OperationResult(false, "Match not found.");
                }

                if (match.Status == MatchStatus.FirstHalf || match.Status == MatchStatus.HalfTime || match.Status == MatchStatus.SecondHalf)
                {
                    return new OperationResult(false, "Match is already in progress.");
                }

                if (match.Status != MatchStatus.Scheduled)
                {
                    return new OperationResult(false, "Match is not in the scheduled status.");
                }

                match.Status = MatchStatus.FirstHalf;
                await repo.SaveChangesAsync();

                await matchJobService.StartMatchJobAsync(matchId);

                return new OperationResult(true, "Match started successfully!");
            }
            catch (Exception)
            {
                return new OperationResult(false, "An error occurred while starting the match.");
            }
        }

        public async Task<MatchSimpleViewModel> GetMatchSimpleViewAsync(int matchId)
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

        public async Task<MatchEndViewModel> GetMatchEndViewAsync(int matchId)
        {
            var match = await GetMatchAsync(matchId);

            var model = new MatchEndViewModel
            {
                MatchId = match.Id,
                HomeTeam = await teamService.GetTeamNameAsync(match.HomeTeamId),
                AwayTeam = await teamService.GetTeamNameAsync(match.AwayTeamId),
                LeagueId = match.LeagueId,
                MatchMinute = matchTimerService.GetMatchMinute(matchId),
                MatchStatus = match.Status
            };

            return model;
        }

        private async Task<Match> GetMatchAsync(int matchId) => await repo.GetByIdAsync<Match>(matchId);

        public async Task<OperationResult> DeleteMatchAsync(int matchId)
        {
            var match = await repo.GetByIdAsync<Match>(matchId);

            if (match == null)
            {
                return new OperationResult(false, "Match not found!");
            }

            if (match.Status != MatchStatus.Scheduled)
            {
                return new OperationResult(false, "You cannot delete a match that has started, finished, or been postponed.");
            }

            await repo.RemoveAsync(match);
            await repo.SaveChangesAsync();

            return new OperationResult(true, $"Successfully deleted {match.Id}!");
        }

        public async Task<int> GetMatchMinuteAsync(int matchId)
        {
            var match = await GetMatchAsync(matchId);

            return match.Minutes;
        }

        public async Task<OperationResult> PauseMatchAsync(int matchId)
        {
            try
            {
                var match = await GetMatchAsync(matchId);
                if (match == null)
                {
                    return new OperationResult(false, "Match not found.");
                }

                if (matchTimerService.GetMatchMinute(matchId) <= Constants.MessageConstants.HalfTimeMinute)
                {
                    return new OperationResult(false, "The match is too early for half time.");
                }

                if (match.Status is MatchStatus.Scheduled or MatchStatus.Postponed or MatchStatus.SecondHalf or MatchStatus.HalfTime or MatchStatus.Finished)
                {
                    string reason = match.Status switch
                    {
                        MatchStatus.Scheduled => "The match has not started yet.",
                        MatchStatus.Postponed => "The match is postponed.",
                        MatchStatus.SecondHalf => "It's the second half of the match, halftime is over.",
                        MatchStatus.HalfTime => "The match is already at halftime.",
                        MatchStatus.Finished => "The match is finished.",
                        _ => "The match cannot be paused at this time."
                    };

                    return new OperationResult(false, reason);
                }

                if (matchTimerService.GetMatchMinute(matchId) <= Constants.MessageConstants.HalfTimeMinute)
                {
                    return new OperationResult(false, "The match is too early for half time.");
                }

                match.Status = MatchStatus.HalfTime;
                match.Minutes = Constants.MessageConstants.HalfTimeMinute;
                await repo.SaveChangesAsync();

                await matchJobService.PauseMatchJobAsync(matchId);

                var cacheKey = $"Match_{matchId}_Status";
                memoryCache.Set(cacheKey, match.Status);

                return new OperationResult(true, "Match paused successfully!");
            }
            catch (Exception)
            {
                return new OperationResult(false, "An error occurred while starting the match.");
            }
        }

        public async Task<OperationResult> UnpauseMatchAsync(int matchId)
        {
            try
            {
                var match = await GetMatchAsync(matchId);

                if (match == null)
                {
                    return new OperationResult(false, "Match not found.");
                }

                if (match.Status == MatchStatus.SecondHalf)
                {
                    return new OperationResult(false, "Second half is already in progress.");
                }

                if (match.Status != MatchStatus.HalfTime)
                {
                    return new OperationResult(false, "The match is not at half time.");
                }

                match.Status = MatchStatus.SecondHalf;
                await repo.SaveChangesAsync();

                await matchJobService.UnpauseMatchJobAsync(matchId);

                var cacheKey = $"Match_{matchId}_Status";
                memoryCache.Set(cacheKey, match.Status);

                return new OperationResult(true, "Match paused successfully!");
            }
            catch (Exception)
            {
                return new OperationResult(false, "An error occurred while starting the match.");
            }
        }

        public async Task<OperationResult> EndMatchAsync(int matchId)
        {
            try
            {
                var match = await GetMatchAsync(matchId);

                if (match == null)
                {
                    return new OperationResult(false, "Match not found.");
                }

                if (match.Status is MatchStatus.Postponed or MatchStatus.Finished)
                {
                    string reason = match.Status switch
                    {
                        MatchStatus.Postponed => "The match is postponed.",
                        MatchStatus.Finished => "The match is already finished.",
                        _ => "The match cannot be ended at this time."
                    };

                    return new OperationResult(false, reason);
                }

                if (matchTimerService.GetMatchMinute(matchId) < Constants.MessageConstants.FullTimeMinute 
                        || match.Status == MatchStatus.FirstHalf 
                        || match.Status == MatchStatus.Scheduled)
                {
                    match.Status = MatchStatus.Postponed;
                }
                else
                {
                    match.Status = MatchStatus.Finished;
                }

                match.Minutes = matchTimerService.GetMatchMinute(matchId);
                await repo.SaveChangesAsync();

                await matchJobService.EndMatchJobAsync(matchId);

                var statusCacheKey = $"Match_{matchId}_Status";
                var minuteCacheKey = $"Match_{matchId}_Minute";
                memoryCache.Remove(statusCacheKey);
                memoryCache.Remove(minuteCacheKey);

                return new OperationResult(true, $"Match {match.Status} successfully!");
            }
            catch (Exception)
            {
                return new OperationResult(false, "An error occurred while starting the match.");
            }
        }

        public async Task<MatchStatus> GetMatchStatusAsync(int matchId) => (await repo.GetByIdAsync<Match>(matchId)).Status;

        public async Task<OperationResult> AddFavoriteAsync(int matchId)
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return new OperationResult(false, "User must be logged in.");
            }

            var favoriteExists = await repo.AllReadonly<Favorite>().AnyAsync(f => f.UserId == userId && f.MatchId == matchId);

            if(!favoriteExists)
            {
                var favorite = new Favorite
                {
                    UserId = userId,
                    MatchId = matchId
                };

                await repo.AddAsync(favorite);
                await repo.SaveChangesAsync();
            }
            else
            {
                return new OperationResult(false, "The match is already added to favorites.");
            }

            return new OperationResult(true, "The match successfully added to favorites.");
        }

        public async Task<OperationResult> RemoveFavoriteAsync(int matchId)
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return new OperationResult(false, "User must be logged in.");
            }

            var favorite = await repo.AllReadonly<Favorite>().FirstOrDefaultAsync(f => f.UserId == userId && f.MatchId == matchId);

            if (favorite != null)
            {
                await repo.RemoveAsync(favorite);
                await repo.SaveChangesAsync();

                return new OperationResult(true, "Match removed from favorites successfully.");
            }
            else
            {
                return new OperationResult(false, "Favorite not found.");
            }
        }
    }
}
