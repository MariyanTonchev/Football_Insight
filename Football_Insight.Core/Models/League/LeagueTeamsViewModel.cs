using Football_Insight.Core.Models.Team;

namespace Football_Insight.Core.Models.League
{
    public class LeagueTeamsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<TeamSimpleViewModel> Teams { get; set; } = new List<TeamSimpleViewModel>();
    }
}
