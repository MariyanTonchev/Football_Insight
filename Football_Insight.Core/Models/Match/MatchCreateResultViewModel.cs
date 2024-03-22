namespace Football_Insight.Core.Models.Match
{
    public class MatchCreateResultViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int LeagueId { get; set; }
    }
}
