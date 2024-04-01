namespace Football_Insight.Core.Models.Match
{
    public class MatchSimpleViewModel
    {
        public int MatchId { get; set; }

        public string HomeTeam { get; set; } = string.Empty;

        public string AwayTeam { get; set; } = string.Empty;

        public int LeagueId { get; set; }

        public override string ToString()
        {
            return $"{HomeTeam} VS {AwayTeam}";
        }
    }
}
