using Football_Insight.Core.Contracts;
using Football_Insight.Core.Models.Dashboard;

namespace Football_Insight.Core.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IPlayerService playerService;
        private readonly IAccountService accountService;

        public DashboardService(IPlayerService _playerService, IAccountService _accountService)
        {
            playerService = _playerService;
            accountService = _accountService;
        }

        public async Task<DashboardViewModel> GetDashboardViewModelAsync()
        {
            var viewModel = new DashboardViewModel
            {
                TopGoalScorers = await playerService.GetTopScorersAsync(),
                TopAssisters = await playerService.GetTopAssistersAsync(),
                FavoriteMatches = await accountService.GetFavoriteMatchesAsync(),
            };

            return viewModel;
        }
    }
}
