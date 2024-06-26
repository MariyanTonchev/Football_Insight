﻿using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Match;
using Football_Insight.Infrastructure.Data.Enums;

namespace Football_Insight.Core.Contracts
{
    public interface IMatchService
    {
        Task<int> CreateMatchAsync(MatchFormViewModel model, int leagueId);

        Task<OperationResult> UpdateMatchAsync(MatchFormViewModel model, int matchId);

        Task<MatchDetailsViewModel> GetMatchDetailsAsync(int matchId);

        Task<MatchFormViewModel?> GetMatchFormViewModelByIdAsync(int id);

        Task<MatchSimpleViewModel> GetMatchSimpleViewAsync(int matchId);

        Task<MatchEndViewModel> GetMatchEndViewAsync(int matchId);

        Task<MatchStatus> GetMatchStatusAsync(int matchId);

        Task<OperationResult> StartMatchAsync(int matchId);

        Task<OperationResult> PauseMatchAsync(int matchId);

        Task<OperationResult> UnpauseMatchAsync(int matchId);

        Task<OperationResult> EndMatchAsync(int matchId);

        Task<OperationResult> DeleteMatchAsync(int matchId);

        Task<OperationResult> AddFavoriteAsync(int matchId);

        Task<OperationResult> RemoveFavoriteAsync(int mathcId);

        Task<int> GetMatchMinuteAsync(int matchId);
    }
}
