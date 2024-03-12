using Football_Insight.Models.Team;

namespace Football_Insight.Models.League
{
    public class LeagueTeamsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<TeamSimpleViewModel> Teams { get; set; } = new List<TeamSimpleViewModel>();
    }
}
