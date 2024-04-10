using Football_Insight.Core.Models;
using Football_Insight.Core.Models.Player;
using Football_Insight.Core.Models.Team;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Football_Insight.Core.Contracts
{
    public interface IPlayerService
    {
        PlayerFormViewModel GetCreateFomViewModel(int teamId);

        Task<OperationResult> CreatePlayerAsync(PlayerFormViewModel viewModel);

        Task<PlayerFormViewModel> GetEditFormViewModel(int playerId);

        Task<OperationResult> UpdatePlayerAsync(PlayerFormViewModel viewModel, int playerId);

        Task<PlayerSimpleViewModel> GetPlayerSimpleViewModelAsync(int playerId);

        Task<OperationResult> DeletePlayerAsync(int playerId);

        ICollection<SelectListItem> GetPositionsFromEnum();
    }
}
