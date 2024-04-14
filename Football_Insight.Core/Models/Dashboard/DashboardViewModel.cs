using Football_Insight.Core.Models.Match;
using Football_Insight.Core.Models.Player;

namespace Football_Insight.Core.Models.Dashboard
{
    public class DashboardViewModel
    {
        public List<PlayerWidgetViewModel> TopGoalScorers = new List<PlayerWidgetViewModel>();

        public List<PlayerWidgetViewModel> TopAssisters = new List<PlayerWidgetViewModel>();

        public List<MatchFavoriteViewModel> FavoriteMatches = new List<MatchFavoriteViewModel>();
    }
}
