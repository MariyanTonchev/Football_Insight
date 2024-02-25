using Microsoft.AspNetCore.Mvc.Rendering;

namespace Football_Insight.Models.League
{
    public class LeagueViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
