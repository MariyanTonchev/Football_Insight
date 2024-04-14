using Football_Insight.Core.Models.Dashboard;

namespace Football_Insight.Core.Contracts
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardViewModelAsync();
    }
}
